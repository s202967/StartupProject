using StartupProject.Core.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace StartupProject.EntityFramework.EntityFramework
{
    public static class ChangeTrackerExtensions
    {
        public static void ProcessCreation(this ChangeTracker changeTracker, Guid? userId)
        {
            foreach (var item in changeTracker.Entries<IHasCreator>().Where(e => e.State == EntityState.Added))
            {
                item.Entity.CreatedBy = userId;
            }

            foreach (var item in changeTracker.Entries<ICreatedOn>().Where(e => e.State == EntityState.Added))
            {
                item.Entity.CreatedOn = DateTime.UtcNow;
            }
        }

        public static void ProcessModification(this ChangeTracker changeTracker, Guid? userId)
        {
            foreach (var item in changeTracker.Entries<IHasModifier>().Where(e => e.State == EntityState.Modified))
            {
                item.Entity.ModifiedBy = userId;
            }

            foreach (var item in changeTracker.Entries<IModifiedOn>().Where(e => e.State == EntityState.Modified))
            {
                item.Entity.ModifiedOn = DateTime.UtcNow;
            }
        }

        public static void ProcessDeletion(this ChangeTracker changeTracker, Guid? userId)
        {
            foreach (var item in changeTracker.Entries<IHasDeleter>().Where(e => e.State == EntityState.Deleted))
            {
                item.Entity.DeletedBy = userId;
            }

            foreach (var item in changeTracker.Entries<IDeletedOn>().Where(e => e.State == EntityState.Deleted))
            {
                item.Entity.DeletedOn = DateTime.UtcNow;
            }

            foreach (var item in changeTracker.Entries<ISoftDelete>().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.Entity.IsDeleted = true;
            }
        }
    }
}
