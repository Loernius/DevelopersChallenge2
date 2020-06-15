using Desafio.Database.Interface;
using Desafio.Database.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Desafio.Database
{
    public class DesafioContext : DbContext, IDesafioContext
    {
        public DesafioContext() { }

        public DesafioContext(DbContextOptions<DesafioContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //IConfigurationRoot config = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .AddJsonFile($"appsettings.{env}.json")
            //    .Build();


            //optionsBuilder.UseSqlServer(config.GetConnectionString("DesafioContext"));
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureAllEntityTypes(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.SeedModel();
        }

        protected void ConfigureAllEntityTypes(ModelBuilder modelBuilder)
        {
            var applyConfigMethod = typeof(ModelBuilder).
            GetMethods()
            .Where(e => e.Name == "ApplyConfiguration" && e.GetParameters().Single().ParameterType.Name == typeof(IEntityTypeConfiguration<>).Name)
            .Single();
            var configs = GetAllConfigs();

            foreach (var item in configs)
            {
                var entityType = item.ImplementedInterfaces.First(x => x.Name == typeof(IEntityTypeConfiguration<>).Name).GenericTypeArguments.Single();
                var applyConfigGenericMethod = applyConfigMethod.MakeGenericMethod(entityType);
                applyConfigGenericMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(item) });
            }
        }

        protected IList<TypeInfo> GetAllConfigs()
            => Assembly.GetExecutingAssembly().DefinedTypes
                .Where(t => t.ImplementedInterfaces.Any(i => i.Name == typeof(IDesafioContextConfiguration).Name))
                .Where(i => i.IsClass && !i.IsAbstract && !i.IsNested)
                .ToList();



    }
}
