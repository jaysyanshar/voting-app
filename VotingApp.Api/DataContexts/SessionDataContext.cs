using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public class SessionDataContext : DataContextBase<Session, SessionDataContext>
    {

        public SessionDataContext( DbContextOptions<SessionDataContext> options ) : base( options )
        {
        }
    }
}