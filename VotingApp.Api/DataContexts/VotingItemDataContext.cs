using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public class VotingItemDataContext : DataContextBase<VotingItem, VotingItemDataContext>
    {
        public VotingItemDataContext( DbContextOptions<VotingItemDataContext> options ) : base( options )
        {
        }
    }
}