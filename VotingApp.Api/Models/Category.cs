using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApp.Api.Models
{
    public class Category : Core.Models.Category
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public override string Id { get; set; }

        [Required] public override string Name { get; set; }

    }
}