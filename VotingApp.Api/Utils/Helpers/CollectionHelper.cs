using System.Collections.Generic;
using System.Linq;

namespace VotingApp.Api.Utils.Helpers
{
    public static class CollectionHelper
    {
        public static IEnumerable<int> ParseToIntNumbers( this string data, string separator )
        {
            if( string.IsNullOrEmpty( data ) )
                return new List<int>();

            string[] values = data.Split( separator );
            return ( from value in values select int.Parse( value ) ).ToList();
        }

        public static string ConvertToString<T>( this IEnumerable<T> data, string separator )
        {
            return string.Join( separator, data );
        }
    }
}