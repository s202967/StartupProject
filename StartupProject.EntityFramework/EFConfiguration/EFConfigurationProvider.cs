using StartupProject.EntityFramework.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace StartupProject.EntityFramework.EFConfiguration
{
    public class EFConfigurationProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _options;

        public EFConfigurationProvider(Action<DbContextOptionsBuilder> options)
        {
            _options = options;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _options(builder);
        }
    }
}
