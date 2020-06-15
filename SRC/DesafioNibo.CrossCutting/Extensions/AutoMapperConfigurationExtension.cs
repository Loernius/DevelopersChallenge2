using AutoMapper;
using AutoMapper.Configuration;
using Desafio.CrossCutting.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Desafio.CrossCutting.Extensions
{
    public static class AutoMapperConfigurationExtension
    {
        public static void ConfigureMapping(this MapperConfigurationExpression config)
        {
            config.AllowNullCollections = false;
            AutoAdd(config, GetAllProfiles());
        }

        public static void AutoAdd(MapperConfigurationExpression config, List<Type> items)
        {
            foreach (var item in items)
                config.AddProfile(Activator.CreateInstance(item) as Profile);
        }

        public static List<Type> GetAllProfiles()
            => Assembly.GetAssembly(typeof(ExtratoProfile)).GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x)).ToList();

    }
}
