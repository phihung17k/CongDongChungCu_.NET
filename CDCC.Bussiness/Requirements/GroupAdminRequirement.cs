using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCC.Bussiness.Requirements
{
    public class GroupAdminRequirement : IAuthorizationRequirement
    {
        public GroupAdminRequirement(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public bool IsAdmin { get; set; }
    }
}
