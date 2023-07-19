using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IdentityResult> CreateUserAsync(CreateUserModel request);
    }
}