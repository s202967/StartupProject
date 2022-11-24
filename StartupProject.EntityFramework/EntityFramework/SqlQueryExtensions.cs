using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace StartupProject.EntityFramework.EntityFramework
{
    public static class SqlQueryExtensions
    {
        public static IList<TOutput> SqlQuery<TOutput>(this DbContext db, string sql, params object[] parameters) where TOutput : class
        {
            var output = new List<TOutput>();

            using (var dbContext = new ContextForQueryType<TOutput>(db.Database.GetDbConnection()))
            {
                var strategy = dbContext.Database.CreateExecutionStrategy();
                strategy.Execute(() =>
                {
                    if (db.Database.CurrentTransaction != null)
                        dbContext.Database.UseTransaction(db.Database.CurrentTransaction.GetDbTransaction());

                    output = dbContext.Set<TOutput>().FromSqlRaw(sql, parameters).ToList();

                });

            }
            return output;
        }

        private class ContextForQueryType<TOutput> : DbContext where TOutput : class
        {
            private readonly DbConnection _connection;

            public ContextForQueryType(DbConnection connection)
            {
                _connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connection, options => options.EnableRetryOnFailure());

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<TOutput>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
