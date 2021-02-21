using System.ComponentModel.DataAnnotations;

namespace VotingApp.Api.Models
{
    public class Client : Core.Models.Client
    {
        [Key] public override string Email { get; set; }
        [Required] public override string Password { get; set; }
        [Required] public override string FirstName { get; set; }
        public override string LastName { get; set; }
        [Required] public override int Age { get; set; }
        [Required] public override string Gender { get; set; }
    }
}