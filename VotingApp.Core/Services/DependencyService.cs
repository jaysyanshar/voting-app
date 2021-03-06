using System.Collections.Generic;

namespace VotingApp.Core.Services
{
    public static class DependencyService
    {
        private static readonly IDictionary<string, object> _container = new Dictionary<string, object>();

        public static void RegisterSingleton<TService, TImplementation>( TImplementation implementation )
            where TImplementation : class, TService
        {
            _container.Add( nameof( TService ), implementation );
        }

        public static TService Resolve<TService>()
        {
            return ( TService ) _container[nameof( TService )];
        }
    }
}