using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Api.Models
{
    public class Session : Core.Models.Session
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public override string Id { get; set; }

        [Required] public override string IpAddress { get; set; }
        [Required] public override string UserEmail { get; set; }
        [Required] public override string UserRole { get; set; }
        [Required] public override bool LoggedIn { get; set; }

    }
}