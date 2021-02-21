using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public class VoteRecordDataContext : DataContextBase<VoteRecord, VoteRecordDataContext>
    {

        public VoteRecordDataContext( DbContextOptions<VoteRecordDataContext> options ) : base( options )
        {
        }
    }
}