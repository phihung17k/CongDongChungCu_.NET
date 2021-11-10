using CDCC.Bussiness.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDCC.Bussiness.JWT
{
    public class SystemAdminRequirementHandler : AuthorizationHandler<SystemAdminRequirement>
    {
        bool isChecked = false;
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SystemAdminRequirement requirement)
        {
            //case 1: only isSystemAdmin or only isAdmin
            //case 2: both isSystemAdmin and isAdmin
            int count = 0;
            IEnumerable<IAuthorizationRequirement> enumerable = context.Requirements;
            List<IAuthorizationRequirement> list = enumerable.ToList();
            count = list.Count();

            bool checkIsSystemAdmin = context.User.HasClaim(claim => claim.Type == "isSystemAdmin");
            bool checkIsAdmin = context.User.HasClaim(claim => claim.Type == "isAdmin");
            if(checkIsSystemAdmin || checkIsAdmin)
            {
                if(checkIsSystemAdmin)
                {
                    bool.TryParse(context.User.FindFirst(c => c.Type == "isSystemAdmin").Value, out bool actualIsSystemAdmin);
                    if(count == 1)
                    {
                        if (actualIsSystemAdmin == requirement.IsSystemAdmin)
                        {
                            context.Succeed(requirement);
                        }
                    } else if (count == 2)
                    {
                        if (actualIsSystemAdmin)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
                else
                {
                    bool.TryParse(context.User.FindFirst(c => c.Type == "isAdmin").Value, out bool actualIsAdmin);
                    if(count == 1)
                    {
                        if (actualIsAdmin == requirement.IsAdmin)
                        {
                            context.Succeed(requirement);
                        }
                    } else if (count == 2)
                    {
                        if (actualIsAdmin)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
            //if (!context.User.HasClaim(claim => claim.Type == "isSystemAdmin"))
            //{
            //    return Task.CompletedTask;
            //}

            //if (!bool.TryParse(context.User.FindFirst(c => c.Type == "isSystemAdmin").Value, out bool actualIsSystemAdmin))
            //{
            //    return Task.CompletedTask;
            //}
            ////system admin
            //if (actualIsSystemAdmin)
            //{
            //    context.Succeed(requirement);
            //}
            //else
            //{
            //    //group admiin
            //    //string temp = context.User.FindFirst(c => c.Type == "residentJson").Value;
            //    context.Succeed(requirement);
            //}
            return Task.CompletedTask;
        }
    }
}
