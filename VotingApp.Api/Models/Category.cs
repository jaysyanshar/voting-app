using System.ComponentModel.DataAnnotations;

namespace VotingApp.Api.Models
{
    public class Category : IModel<int>
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public bool ValidateFields()
        {
            return Id > 0 && !string.IsNullOrWhiteSpace( Name );
        }
    }
}