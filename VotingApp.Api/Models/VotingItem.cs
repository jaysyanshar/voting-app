using System;
using System.ComponentModel.DataAnnotations;

namespace VotingApp.Api.Models
{
    public class VotingItem : IModel<long>
    {
        [Key] public long Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public DateTime CreatedDate { get; set; }
        [Required] public long VotersCount { get; set; }
        [Required] public DateTime DueDate { get; set; }
        [Required] public string Categories { get; set; }

        public long GetKey()
        {
            return Id;
        }

        public bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace( Name ) &&
                   CreatedDate < DueDate &&
                   VotersCount >= 0 &&
                   !string.IsNullOrWhiteSpace( Categories );
        }
    }
}