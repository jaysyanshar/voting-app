namespace VotingApp.Core.Models
{
    public class Category : IModel<string>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public string GetKey()
        {
            return Id;
        }

        public bool ValidateFields()
        {
            return !string.IsNullOrWhiteSpace( Name );
        }
    }
}