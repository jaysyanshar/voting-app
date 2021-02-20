using System.Text.RegularExpressions;

namespace VotingApp.Api.Utils.Validators
{
    public class IpValidator : IValidator<string>
    {

        public bool Validate( string data )
        {
            if( string.IsNullOrWhiteSpace( data ) )
                return false;

            Regex ip = new(
                @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" );

            return ip.IsMatch( data );
        }
    }
}