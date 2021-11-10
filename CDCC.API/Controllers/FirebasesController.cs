using Microsoft.AspNetCore.Mvc;
using System;
using FirebaseAdmin.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;
using CDCC.Bussiness.Catalog.Users;
using CDCC.Bussiness.ViewModels.User;
using System.Text;
using CDCC.Bussiness.Catalog.Residents;
using CDCC.Bussiness.JWT;
using FirebaseAdmin.Messaging;
using CDCC.Bussiness.ViewModels;

namespace CDCC.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FirebasesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IResidentService residentService;

        public FirebasesController(IUserService userService, IResidentService residentService)
        {
            _userService = userService;
            this.residentService = residentService;
        }

        [HttpPost]
        public async Task<ActionResult> AuthenticateUser()
        {
            try
            {
                var header = Request.Headers;
                string idToken = "";
                if (header.ContainsKey("Authorization"))
                {
                    String tempID = header["Authorization"].ToString();
                    idToken = tempID.Split(" ")[1];
                }

                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                FirebaseToken decodedToken = await auth.VerifyIdTokenAsync(idToken);
                //string uid = decodedToken.Uid;
                IReadOnlyDictionary<string, dynamic> info = decodedToken.Claims;
                string email = info["email"];
                UserViewModel user = await _userService.GetUserByEmail(email);
                string clientToken = "";
                //if user = null => must be resident => isAdmin = false
                if (user == null)
                {
                    //set default pass = email convert base64
                    var plainTextBytes = Encoding.UTF8.GetBytes(email);
                    string password = Convert.ToBase64String(plainTextBytes);
                    //set username = email - @gmail.com
                    string username = email.Split("@")[0];
                    UserInsertModel insertedUser = new UserInsertModel()
                    {
                        Username = username,
                        Password = password,
                        Fullname = info["name"],
                        Phone = "",
                        Email = email,
                        IsSystemAdmin = false,
                        Status = true
                    };
                    UserViewModel temp = await _userService.Insert(insertedUser);
                    clientToken = JWTGeneration.GenerateJSONWebTokenAsync(temp, false);
                }
                else
                {
                    if ((bool)user.Status == false)
                    {
                        return StatusCode(403, "The account is banned");
                    }
                    if (user.IsSystemAdmin)
                    {
                        //generate token for user is system admin
                        clientToken = JWTGeneration.GenerateJSONWebTokenAsync(user, true);
                    }
                    else
                    {
                        //generate token for user is group admin and resident 
                        List<int> residentIdList = residentService.GetAllResidentId(user.Id);
                        clientToken = JWTGeneration.GenerateJSONWebTokenAsync(user, false);
                    }
                }
                return Ok(clientToken);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<ActionResult> SendNotification([FromBody] NotificationModel notification)
        {
            try
            {
                if (notification.Title.Length > 0 && notification.Body.Length > 0)
                {
                    var topic = "cdcc";
                    var message = new Message()
                    {
                        Notification = new Notification()
                        {
                            Title = notification.Title,
                            Body = notification.Body
                        },
                        Topic = topic,
                    };
                    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                    return Ok(response);
                }
                return BadRequest("Not found the passed data");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

