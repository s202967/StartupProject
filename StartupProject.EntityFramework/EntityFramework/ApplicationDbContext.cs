using StartupProject.Core.Domain.DbEntity;
using StartupProject.Core.Domain.FunctionEntity;
using StartupProject.Core.Infrastructure.Localization;
using StartupProject.Core.Security.Identity;
using StartupProject.Core.Security.UserActivity;
using StartupProject.EntityFramework.Security.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject.EntityFramework.EntityFramework
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        public Guid UserId { get; set; }

        public DbSet<LocaleStringResource> LocaleStringResources { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<CheckList> checkLists { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Employee> employees { get; set; }

        public virtual void Commit()
        {
            ChangeTracker.DetectChanges();
            ChangeTracker.ProcessModification(UserId);
            ChangeTracker.ProcessDeletion(UserId);
            ChangeTracker.ProcessCreation(UserId);
            base.SaveChanges();
        }

        public virtual async Task CommitAsync(bool isSoftDelete = true)
        {
            ChangeTracker.DetectChanges();
            ChangeTracker.ProcessModification(UserId);

            if (isSoftDelete)
                ChangeTracker.ProcessDeletion(UserId);

            ChangeTracker.ProcessCreation(UserId);
            await base.SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Composite keys can only be configured using the Fluent API

            //Scalar function


            base.OnModelCreating(modelBuilder);
        }
    }
}
