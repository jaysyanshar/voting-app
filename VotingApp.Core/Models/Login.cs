using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
{
    public class Login : User
    {
        public virtual string UserRole { get; set; }

        public override bool ValidateFields()
        {
            UserRole.Type role;
            try
            {
                role = UserRole.ParseEnum<UserRole.Type>();
            }
            catch
            {
                return false;
            }

            return base.ValidateFields() && role != default;
        }
    }
}