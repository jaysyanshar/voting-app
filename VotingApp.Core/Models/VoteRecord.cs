using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
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