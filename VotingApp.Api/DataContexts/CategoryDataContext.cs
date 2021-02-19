using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public class CategoryDataContext : DataContextBase<Category, CategoryDataContext>
    {
        public CategoryDataContext( DbContextOptions<CategoryDataContext> options ) : base( options )
        {
        }
    }
}