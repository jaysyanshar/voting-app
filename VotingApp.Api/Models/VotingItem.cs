using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Api.Models
{
    public class VotingItem : Core.Models.VotingItem
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public override string Id { get; set; }

        public override string Description { get; set; }
        [Required] public override string Name { get; set; }
        [Required] public override DateTime CreatedDate { get; set; }
        [Required] public override long VotersCount { get; set; }
        [Required] public override DateTime DueDate { get; set; }
        [Required] public override string Categories { get; set; }

    }
}