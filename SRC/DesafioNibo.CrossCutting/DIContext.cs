using Microsoft.Extensions.DependencyInjection;
using System;

namespace Desafio.CrossCutting
{
    public class DIContext
    {
        private static IServiceProvider _provider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_provider == null)
                    _provider = _collection.BuildServiceProvider();

                return _provider;
            }
        }

        private static IServiceCollection _collection;
        public static IServiceCollection ServiceCollection
        {
            get
            {
                if (_collection == null)
                    _collection = new ServiceCollection();

                return _collection;
            }
        }

        public static T GetService<T>()
            => ServiceProvider.GetService<T>();
    }
}
