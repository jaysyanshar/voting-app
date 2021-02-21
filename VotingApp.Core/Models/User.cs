using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
{
    public class User : IModel<string>
    {
        [Key] public string Email { get; set; }
        [Required] public string Password { get; set; }

        public string GetKey()
        {
            return Email;
        }

        public virtual bool ValidateFields()
        {
            return ValidationHelper.ValidateEmail( Email ) && 
                   ValidationHelper.ValidatePassword( Password );
        }
    }
}