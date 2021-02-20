using System.ComponentModel.DataAnnotations;
using VotingApp.Api.Utils.Helpers;

namespace VotingApp.Api.Models
{
    public class Client : UserBase
    {
        [Required] public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required] public int Age { get; set; }
        [Required] public string Gender { get; set; }

        public override bool ValidateFields()
        {
            Gender.Type gender;
            try
            {
                gender = Gender.ParseEnum<Gender.Type>();
            }
            catch
            {
                return false;
            }

            return base.ValidateFields() &&
                   ValidationHelper.ValidateName( FirstName ) &&
                   ( string.IsNullOrEmpty( LastName ) || 
                     ValidationHelper.ValidateName( LastName ) ) &&
                   Age > 0 && 
                   gender != default;
        }
    }
}