using System;

namespace VotingApp.Core.Models
{
    public class VotingItem : IModel<string>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual long VotersCount { get; set; }
        public virtual DateTime DueDate { get; set; }
        public virtual string Categories { get; set; }

        public string GetKey()
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