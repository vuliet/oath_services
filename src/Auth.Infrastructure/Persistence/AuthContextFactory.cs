using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace Auth.Infrastructure.Persistence
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {

        public AuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseMySQL("Server=localhost; Port=3306; Database=Identity; Uid=root; Pwd=password123; ConnectionTimeout=60;");
            return new AuthContext(optionsBuilder.Options);
        }

    }
}