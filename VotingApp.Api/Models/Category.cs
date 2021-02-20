namespace VotingApp.Api.Models
{
    public class Category : IModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}