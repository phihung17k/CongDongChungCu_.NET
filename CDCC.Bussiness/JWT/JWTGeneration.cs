using CDCC.Bussiness.ViewModels.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CDCC.Bussiness.JWT
{
    public class JWTGeneration
    {
        //author by id
        //if user is isSystemAdmin => add a claim "isSystemAdmin"
        //if user is group admin or resident => add a claim "isAdmin"
        //1. Với user đăng nhập lần đầu => chỉ được tạo resident và lấy đó làm resident role
        //2. Với user đã đăng nhập => chọn 1 resident trong danh sách resident liên kết với user đó 
        //  - Nếu resident có isAdmin = true => group admin role
        //  - Nếu resident có isAdmin = false => resident

        //for system admin => don't have resident
        public static string GenerateJSONWebTokenAsync(UserViewModel user, bool isSystemAdmin)
        {
            string returnedToken;
            if (isSystemAdmin)
            {
                //system admin
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/congdongchungcu-520e7",
                    audience: "congdongchungcu-520e7",
                    claims: new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username),
                    new Claim("fullname", user.Fullname),
                    new Claim("phone", user.Phone),
                    new Claim("email", user.Email),
                    new Claim("isSystemAdmin", user.IsSystemAdmin.ToString()),
                    new Claim("status", user.Status.ToString())
                    },
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cong dong chung cu")),
                        algorithm: SecurityAlgorithms.HmacSha256
                        )
                );
                returnedToken = (new JwtSecurityTokenHandler()).WriteToken(token);
            } 
            else
            {
                //group admin and resident
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://securetoken.google.com/congdongchungcu-520e7",
                    audience: "congdongchungcu-520e7",
                    claims: new[] {
                        new Claim("id", user.Id.ToString()),
                        new Claim("username", user.Username),
                        new Claim("fullname", user.Fullname),
                        new Claim("phone", user.Phone),
                        new Claim("email", user.Email),
                        new Claim("status", user.Status.ToString())
                    },
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cong dong chung cu")),
                        algorithm: SecurityAlgorithms.HmacSha256
                    )
                );
                returnedToken = (new JwtSecurityTokenHandler()).WriteToken(token);
            }
            return returnedToken;
        }

        //for group admin and resident
        // case user haven't chosen resident yet
        // login -> generate this jwt_token -> for client choose resident
        //public static string GenerateJSONWebTokenAsync(UserViewModel user)
        //{
        //    JwtSecurityToken token = new JwtSecurityToken(
        //        issuer: "https://securetoken.google.com/congdongchungcu-520e7",
        //        audience: "congdongchungcu-520e7",
        //        claims: new[] {
        //            new Claim("id", user.Id.ToString()),
        //            new Claim("username", user.Username),
        //            new Claim("fullname", user.Fullname),
        //            new Claim("phone", user.Phone),
        //            new Claim("email", user.Email),
        //            new Claim("status", user.Status.ToString())
        //        },
        //        expires: DateTime.UtcNow.AddDays(1),
        //        signingCredentials: new SigningCredentials(
        //            key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cong dong chung cu")),
        //            algorithm: SecurityAlgorithms.HmacSha256
        //            )
        //    );
        //    string returnedToken = (new JwtSecurityTokenHandler()).WriteToken(token);
        //    return returnedToken;
        //}

        //choose resident id, generate this token for authorization : isAdmin or resident
        public static string GenerateJSONWebTokenAsync(int userId, int residentId, bool isAdmin)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://securetoken.google.com/congdongchungcu-520e7",
                audience: "congdongchungcu-520e7",
                claims: new[] {
                    new Claim("id", userId.ToString()),
                    new Claim("residentId", residentId.ToString()),
                    new Claim("isAdmin", isAdmin.ToString()),
                },
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cong dong chung cu")),
                    algorithm: SecurityAlgorithms.HmacSha256
                    )
            );
            string returnedToken = (new JwtSecurityTokenHandler()).WriteToken(token);
            return returnedToken;
        }
    }
}
