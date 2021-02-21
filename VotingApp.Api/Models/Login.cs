using System.ComponentModel.DataAnnotations;

namespace VotingApp.Api.Models
{
    public class Login : Core.Models.Login
    {

        [Key] public override string Email { get; set; }
        [Required] public override string Password { get; set; }
        [Required] public override string UserRole { get; set; }
    }
}