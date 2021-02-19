using Microsoft.EntityFrameworkCore;
using VotingApp.Api.Models;

namespace VotingApp.Api.DataContexts
{
    public class ClientDataContext : DataContextBase<Client, ClientDataContext>
    {

        public ClientDataContext( DbContextOptions<ClientDataContext> options ) : base( options )
        {
        }
    }
}