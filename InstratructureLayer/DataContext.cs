using InstratructureLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InstratructureLayer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions contextOptions ) : base(contextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            // Called by parameterless ctor Usually Migrations
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.GetEnvironmentVariable("ContentRootPath"))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            optionsBuilder.UseSqlServer(builder.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            GetType().Assembly.GetTypes()
                .Where(t => !t.GetTypeInfo().IsAbstract && t.GetInterfaces().Contains(typeof(IEntityConfiguration)))
                .ToList()
                .ForEach(t => ((IEntityConfiguration)Activator.CreateInstance(t, new[] { modelBuilder })).Configure());
        }
    }
}
