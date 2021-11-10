using Microsoft.AspNetCore.Authorization;

namespace CDCC.Bussiness.Requirements
{
    public class SystemAdminRequirement : IAuthorizationRequirement
    {
        public SystemAdminRequirement(bool isSystemAdmin, bool isAdmin)
        {
            IsSystemAdmin = isSystemAdmin;
            IsAdmin = isAdmin;
        }

        public bool IsSystemAdmin { get; set; }
        public bool IsAdmin { get; set; }
    }
}
