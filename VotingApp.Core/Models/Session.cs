using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
{
    public class Session : IModel<string>
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public string Id { get; set; }

        [Required] public string IpAddress { get; set; }
        [Required] public string UserEmail { get; set; }
        [Required] public string UserRole { get; set; }
        [Required] public bool LoggedIn { get; set; }

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