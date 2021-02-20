using System.ComponentModel.DataAnnotations;
using VotingApp.Api.Utils.Helpers;

namespace VotingApp.Api.Models
{
    public class Login : User
    {
        [Required] public string UserRole { get; set; }

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