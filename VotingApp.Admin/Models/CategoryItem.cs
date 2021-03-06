using VotingApp.Core.Models;

namespace VotingApp.Admin.Models
{
    public class CategoryItem : Category
    {
        public override string ToString()
        {
            return Name;
        }
    }
}