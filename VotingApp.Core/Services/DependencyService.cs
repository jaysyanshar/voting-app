using System;
using System.Collections.Generic;

namespace VotingApp.Core.Services
{
    public static class DependencyService
    {
        private static readonly IDictionary<string, object> _container = new Dictionary<string, object>();

        public static bool RegisterSingleton<TService, TImplementation>( TImplementation implementation )
            where TImplementation : class, TService
        {
            _container.Add( GetName<TService>(), implementation );
            return _container.ContainsKey( GetName<TService>() );
        }

        public static TService Resolve<TService>()
        {
            try
            {
                return ( TService ) _container[GetName<TService>()];

            }
            catch
            {
                return default;
            }
        }

        public static bool Remove<TService>()
        {
            return _container.Remove( GetName<TService>() );
        }

        private static string GetName<T>()
        {
            return typeof( T ).Name;
        }
    }
}