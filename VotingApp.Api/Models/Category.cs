namespace VotingApp.Api.Models
{
    public class Category : IModel<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long GetKey()
        {
            return Id;
        }
    }
}