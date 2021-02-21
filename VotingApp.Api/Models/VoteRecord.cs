using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotingApp.Api.Utils.Helpers;

namespace VotingApp.Api.Models
{
    public class VoteRecord : IModel<string>
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public string Id { get; set; }

        [Required] public string UserEmail { get; set; }
        [Required] public string VotingItemId { get; set; }

        public string GetKey()
        {
            return Id;
        }

        public bool ValidateFields()
        {
            return ValidationHelper.ValidateEmail( UserEmail ) &&
                   !string.IsNullOrWhiteSpace( VotingItemId );
        }
    }
}