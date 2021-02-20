using System;
using System.Net.Mail;

namespace VotingApp.Api.Utils.Validators
{
    public class EmailValidator : IValidator<string>
    {

        public bool Validate( string data )
        {
            if( string.IsNullOrWhiteSpace( data ) )
                return false;

            try
            {
                MailAddress mail = new( data );
                return mail.Address.Equals( data );
            }
            catch( FormatException )
            {
                return false;
            }
        }
    }
}