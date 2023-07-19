using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Auth.Domain.Common;

namespace Auth.Domain.Entities
{
    public class UserClaim : EntityBase<Guid>
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public User User { get; set; }

    }
}