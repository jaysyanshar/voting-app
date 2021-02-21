using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
{
    public class Client : User
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual string Gender { get; set; }

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