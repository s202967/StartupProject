using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace StartupProject.EntityFramework.EFConfiguration
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddDemoDbProvider(this IConfigurationBuilder configuration, Action<DbContextOptionsBuilder> setup)
        {
            configuration.Add(new EFConfigurationSource(setup));
            return configuration;
        }

        public static void AddDbConfiguration(IConfigurationBuilder builder)
        {
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DbConnectionString");
            builder.AddDemoDbProvider(options => options.UseSqlServer(connectionString));
        }
    }
}
