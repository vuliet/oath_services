using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Entities
{
    public class Role : IdentityRole
    {
        public bool IsDeleted { get; set; }

        public IList<RoleClaim> Claims { get; set; }

        public IList<UserRole> UserRoles { get; set; }
    }
}