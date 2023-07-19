using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _context;
        private readonly UserManager<User> _userManager;

        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(AuthContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserModel request)
        {
            var user = new User();
            user.UserName = request.UserName;
            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }
    }
}