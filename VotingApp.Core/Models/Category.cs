namespace VotingApp.Core.Models
{
    public class Category : IModel<string>
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public string Id { get; set; }

        [Required] public string Name { get; set; }

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