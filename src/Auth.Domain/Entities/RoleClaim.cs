using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Domain.Entities
{
    public class RoleClaim : EntityBase<Guid>
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public Role Role { get; set; }
    }
}