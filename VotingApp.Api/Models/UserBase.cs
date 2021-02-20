using System.ComponentModel.DataAnnotations;
using VotingApp.Api.Utils.Helpers;

namespace VotingApp.Api.Models
{
    public abstract class UserBase : IModel<string>
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