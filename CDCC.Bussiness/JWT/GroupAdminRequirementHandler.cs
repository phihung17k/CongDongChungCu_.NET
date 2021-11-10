using CDCC.Bussiness.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CDCC.Util.ErrorResponse;
using System;

namespace CDCC.Bussiness.JWT
{
    public class GroupAdminRequirementHandler : AuthorizationHandler<GroupAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupAdminRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "isAdmin"))
            {

                return Task.CompletedTask;
            }

            if (!bool.TryParse(context.User.FindFirst(c => c.Type == "isAdmin").Value, out bool actualIsSystemAdmin))
            {
                return Task.CompletedTask;
            }

            if (actualIsSystemAdmin == requirement.IsAdmin)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
