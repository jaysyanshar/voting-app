using System.ComponentModel.DataAnnotations;

namespace VotingApp.Api.Models
{
    public abstract class UserBase : IModel<string>
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }

        public string GetKey()
        {
            return Email;
        }
    }
}