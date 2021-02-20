using System.Text.RegularExpressions;

namespace VotingApp.Api.Utils.Validators
{
    public class PasswordValidator : IValidator<string>
    {

        public bool Validate( string data )
        {
            if( string.IsNullOrWhiteSpace( data ) )
                return false;

            Regex uppercase = new( @"[A-Z]+" );
            Regex lowercase = new( @"[a-z]+" );
            Regex number = new( @"[0-9]+" );
            Regex min8Char = new( @".{8,}" );

            return number.IsMatch( data ) &&
                   uppercase.IsMatch( data ) &&
                   lowercase.IsMatch( data ) &&
                   min8Char.IsMatch( data );
        }
    }
}