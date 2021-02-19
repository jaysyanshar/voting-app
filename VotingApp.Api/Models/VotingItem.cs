using System;

namespace VotingApp.Api.Models
{
    public class VotingItem : IModel<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public long VotersCount { get; set; }
        public DateTime DueDate { get; set; }
        public Category[] Categories { get; set; }

        public long GetKey()
        {
            return Id;
        }
    }
}