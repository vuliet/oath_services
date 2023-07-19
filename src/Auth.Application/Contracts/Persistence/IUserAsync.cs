using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Application.Contracts.Persistence
{
    public interface IUserAsync
    {
        Task<bool> CreateAsync(string username);
    }
}