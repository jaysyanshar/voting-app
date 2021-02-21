using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Api.Models
{
    public class VoteRecord : Core.Models.VoteRecord
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public override string Id { get; set; }

        [Required] public override string UserEmail { get; set; }
        [Required] public override string VotingItemId { get; set; }

    }
}