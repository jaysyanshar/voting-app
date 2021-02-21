using System.Text.RegularExpressions;

namespace VotingApp.Core.Utils.Validators
{
    public class PasswordValidator : IValidator<string>
    {

        public bool Validate( string data )
        {
            if( string.IsNullOrWhiteSpace( data ) )
                return false;

            Regex uppercase = new Regex( @"[A-Z]+" );
            Regex lowercase = new Regex( @"[a-z]+" );
            Regex number = new Regex( @"[0-9]+" );
            Regex min8Char = new Regex( @".{8,}" );

            return number.IsMatch( data ) &&
                   uppercase.IsMatch( data ) &&
                   lowercase.IsMatch( data ) &&
                   min8Char.IsMatch( data );
        }
    }
}