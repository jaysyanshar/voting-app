using System.ComponentModel.DataAnnotations;
using VotingApp.Api.Utils.Helpers;

namespace VotingApp.Api.Models
{
    public class VoteRecord : IModel<long>
    {
        [Key] public long Id { get; set; }
        [Required] public string UserEmail { get; set; }
        [Required] public long VotingItemId { get; set; }

        public long GetKey()
        {
            return Id;
        }

        public bool ValidateFields()
        {
            return ValidationHelper.ValidateEmail( UserEmail ) &&
                   VotingItemId > 0;
        }
    }
}