using Newtonsoft.Json;

namespace VotingApp.Core.Utils.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize<T>( T data, bool pretty = false ) where T : class
        {
            return JsonConvert.SerializeObject( data, pretty ? Formatting.Indented : default );
        }

        public static T Deserialize<T>( string data ) where T : class
        {
            return JsonConvert.DeserializeObject<T>( data );
        }
    }
}