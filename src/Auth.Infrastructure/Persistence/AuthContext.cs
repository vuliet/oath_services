using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.Common;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OpenIddict.EntityFrameworkCore.Models;

namespace Auth.Infrastructure.Persistence
{
    public class AuthContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        // public AuthContext(DbContextOptions<AuthContext> options, IDbContextTransaction currentTransaction) : base(options)
        // {
        //     _currentTransaction = currentTransaction;
        // }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreApplication> OpenIddictEntityFrameworkCoreApplications { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreAuthorization> OpenIddictEntityFrameworkCoreAuthorizations { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreScope> OpenIddictEntityFrameworkCoreScopes { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreToken> OpenIddictEntityFrameworkCoreTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.UseOpenIddict();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // foreach (var entry in ChangeTracker.Entries<AuditableEntityBase>())
            // {
            //     switch (entry.State)
            //     {
            //         case EntityState.Added:
            //             entry.Entity.Created = DateTime.Now;
            //             entry.Entity.CreatedBy = "swn";
            //             break;
            //         case EntityState.Modified:
            //             entry.Entity.LastModified = DateTime.Now;
            //             entry.Entity.LastModifiedBy = "swn";
            //             break;
            //     }
            // }
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}