using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool IsDeleted { get; set; }
    }
}