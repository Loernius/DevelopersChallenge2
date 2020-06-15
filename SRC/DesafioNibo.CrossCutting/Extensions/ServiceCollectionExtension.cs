using Desafio.Database;
using Desafio.Database.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Desafio.Database.Repository;
using Desafio.Service;
using Desafio.Domain.Repository.Abstract;
using Desafio.Domain.Service.Abstract;

namespace Desafio.CrossCutting.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool scoped = true)
        {

            services.AddAllScoped(GetRepositories());
            services.AddAllScoped(GetAllServices());

            services.AddScoped<IDesafioContext, DesafioContext>();
            services.AddDbContext<DesafioContext>(opt => opt.UseSqlite(configuration.GetConnectionString("DesafioContext")));
        }
        public static void AddAllTransient(this IServiceCollection services, IList<TypeInfo> items)
        {
            foreach (var item in items)
                services.AddTransient(item.GetInterfaces().First(), item.AsType());
        }

        public static void AddAllScoped(this IServiceCollection services, IList<TypeInfo> items)
        {
            foreach (var item in items)
                services.AddScoped(item.GetInterfaces().First(), item.AsType());
        }

        public static IList<TypeInfo> GetRepositories()
            => Assembly.GetAssembly(typeof(TransactionRepository)).DefinedTypes
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsNested && t.ImplementedInterfaces.Any(i => i.Name == typeof(IRepositoryBase<,>).Name))
            .ToList();

        public static IList<TypeInfo> GetAllServices()
            => Assembly.GetAssembly(typeof(TransactionService)).DefinedTypes
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsNested && t.ImplementedInterfaces.Any(i => i.Name == typeof(IServiceBase<,>).Name))
            .ToList();
    }
}
