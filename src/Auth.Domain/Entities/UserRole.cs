using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Domain.Entities
{
    public class UserRole : EntityBase<Guid>
    {

        public User User { get; set; }

        public Role Role { get; set; }
    }
}