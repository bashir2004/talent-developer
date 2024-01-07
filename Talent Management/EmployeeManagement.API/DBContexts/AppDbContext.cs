using EmployeeManagement.API.Models.Base;
using EmployeeManagement.API.Models;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeeManagement.API.Helpers;

namespace EmployeeManagement.API.DBContexts
{
    public class AppDbContext : DbContext
    {
        private static readonly Type _appDbContextType = typeof(AppDbContext);
        private static MethodInfo _configureEntityDefaultsMethodInfo = _appDbContextType.GetMethod(nameof(ConfigureEntityDefaults), BindingFlags.Instance | BindingFlags.NonPublic);
        private IHttpContextAccessor _contextAccessor;

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public virtual DbSet<AuthUser> AuthUsers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        public override int SaveChanges()
        {
            try
            {
                ApplyEntityActions();
                return base.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                ApplyEntityActions();
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthUser>().HasData(
                //new AuthUser { Id = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), FirstName = "Muhammad", LastName = "Bashir", Email = "bashir@gmail.com", Password = "P@ssword!", Salt = "$2a$12$q0Ca/8YSv36lxcWaXaWqwuePx1IhaWm8wonjKaiYPut5vNqUWsfM6", CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null },
                new AuthUser { Id = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), FirstName = "Muhammad", LastName = "Bashir", Email = "bashir@gmail.com", Password = "$2b$10$KUc3rFiumSPThl9Z/qr6heEYGFcQk/YVZsPCEGzpuc2AwvsiEFH6W", Salt = "$2b$10$KUc3rFiumSPThl9Z/qr6he", CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null },
                new AuthUser { Id = new Guid("CFAE8354-C7F7-4BD7-AF31-43FA991B078E"), FirstName = "Le", LastName = "Tuan", Email = "tuan@gmail.com", Password = "$2b$10$KUc3rFiumSPThl9Z/qr6heEYGFcQk/YVZsPCEGzpuc2AwvsiEFH6W", Salt = "$2b$10$KUc3rFiumSPThl9Z/qr6he", CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null },
                new AuthUser { Id = new Guid("D0413C86-36C6-486B-982D-B13FA76B90B9"), FirstName = "Marc", LastName = "Josha", Email = "marc@gmail.com", Password = "$2b$10$KUc3rFiumSPThl9Z/qr6heEYGFcQk/YVZsPCEGzpuc2AwvsiEFH6W", Salt = "$2b$10$KUc3rFiumSPThl9Z/qr6he", CreatedBy = new Guid("A61C0906-B308-4F6E-B860-28F4E3EE8713"), CreatedAt = DateTime.Now, UpdatedBy = null, UpdatedAt = null }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = new Guid("531FD700-37C7-47D6-9055-8474F62BE903"), Email="someone@example.com", Username="JohnDoe", SkillSet= "AspNet, Angular, React",Hobbies="PLaying Games",PhoneNumber="+60182458049", CreatedAt = DateTime.Now,CreatedBy = new Guid(),UpdatedAt = System.DateTime.Now,UpdatedBy = new Guid() }
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                _configureEntityDefaultsMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        #region Private Methods

        private void ConfigureEntityDefaults<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(IEntity).IsAssignableFrom(typeof(TEntity)))
            {
                modelBuilder.Entity<TEntity>(
                    entity =>
                    {
                        entity.Property(e => ((IEntity)e).Id).HasDefaultValueSql("NEWID()");
                        entity.Property(e => ((IEntity)e).CreatedBy).HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");
                        entity.Property(e => ((IEntity)e).CreatedAt).HasDefaultValueSql("getdate()");
                        entity.Property(e => ((IEntity)e).UpdatedBy).HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'");
                        entity.Property(e => ((IEntity)e).UpdatedAt).HasDefaultValueSql("getdate()");
                    });
            }
        }

        private Guid GetCurrentUserId()
        {
            Guid result = Guid.Empty;
            if (_contextAccessor != null && _contextAccessor.HttpContext != null)
            {
                result = _contextAccessor.HttpContext.GetUserId();
            }

            return result;
        }

        private void ApplyEntityActions()
        {
            var userId = GetCurrentUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ApplyEntityAddedActions(entry, userId);
                        break;
                    case EntityState.Modified:
                        ApplyEntityUpdatedActions(entry, userId);
                        break;
                }
            }
        }

        private void ApplyEntityAddedActions(EntityEntry entry, Guid userId)
        {
            if (entry.Entity is IEntity)
            {
                ((IEntity)entry.Entity).CreatedBy = userId;
                ((IEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                ((IEntity)entry.Entity).UpdatedBy = userId;
                ((IEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        private void ApplyEntityUpdatedActions(EntityEntry entry, Guid userId)
        {
            if (entry.Entity is IEntity)
            {
                ((IEntity)entry.Entity).UpdatedBy = userId;
                ((IEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        #endregion

    }
}
