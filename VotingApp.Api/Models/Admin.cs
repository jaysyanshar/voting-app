using System.ComponentModel.DataAnnotations;

namespace VotingApp.Api.Models
{
    public class Admin : Core.Models.Admin
    {
        [Key] public override string Email { get; set; }
        [Required] public override string Password { get; set; }
    }
}