using VotingApp.Core.Utils.Validators;

namespace VotingApp.Core.Utils.Helpers
{
    public static class ValidationHelper
    {
        private static readonly EmailValidator _emailValidator = new EmailValidator();
        private static readonly PasswordValidator _passwordValidator = new PasswordValidator();
        private static readonly NameValidator _nameValidator = new NameValidator();
        private static readonly IpValidator _ipValidator = new IpValidator();

        public static bool ValidateEmail( string email )
        {
            return _emailValidator.Validate( email );
        }

        public static bool ValidatePassword( string password )
        {
            return _passwordValidator.Validate( password );
        }

        public static bool ValidateName( string name )
        {
            return _nameValidator.Validate( name );
        }

        public static bool ValidateIpAddress( string ipAddress )
        {
            return _ipValidator.Validate( ipAddress );
        }
    }
}