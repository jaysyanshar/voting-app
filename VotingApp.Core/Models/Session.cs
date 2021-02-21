using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
{
    public class Session : IModel<string>
    {
        public virtual string Id { get; set; }

        public virtual string IpAddress { get; set; }
        public virtual string UserEmail { get; set; }
        public virtual string UserRole { get; set; }
        public virtual bool LoggedIn { get; set; }

        public string GetKey()
        {
            return Id;
        }

        public bool ValidateFields()
        {
            UserRole.Type userRole;
            try
            {
                userRole = UserRole.ParseEnum<UserRole.Type>();
            }
            catch
            {
                return false;
            }

            return ValidationHelper.ValidateIpAddress( IpAddress ) &&
                   ValidationHelper.ValidateEmail( UserEmail ) &&
                   userRole != default;
        }
    }
}