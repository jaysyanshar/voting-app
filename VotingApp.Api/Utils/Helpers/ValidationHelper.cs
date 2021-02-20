using VotingApp.Api.Utils.Validators;

namespace VotingApp.Api.Utils.Helpers
{
    public static class ValidationHelper
    {
        private static readonly EmailValidator _emailValidator = new();
        private static readonly PasswordValidator _passwordValidator = new();
        private static readonly NameValidator _nameValidator = new();
        private static readonly IpValidator _ipValidator = new();

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