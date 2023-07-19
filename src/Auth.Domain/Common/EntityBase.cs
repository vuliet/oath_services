using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Auth.Domain.Common
{

    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; protected set; }
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}