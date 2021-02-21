using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Models
{
    public class VoteRecord : IModel<string>
    {
        public virtual string Id { get; set; }

        public virtual string UserEmail { get; set; }
        public virtual string VotingItemId { get; set; }

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