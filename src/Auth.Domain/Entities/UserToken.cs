using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using src.Auth.Domain.Common;

namespace Auth.Domain.Entities
{
    public class UserToken : IdentityUserToken<Guid>
    {
        [Key]
        public Guid Id { get; set; }

    }
}