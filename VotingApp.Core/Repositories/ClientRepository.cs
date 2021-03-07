using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Core.Repositories
{
    public class ClientRepository : RepositoryBase
    {
        private const string CLIENT_URI = @"Clients";

        public ClientRepository( HttpClient client ) : base( client )
        {
        }

        public async Task<RepositoryResponse<Client>> Register( Client client )
        {
            HttpContent content = CreateContent( client );
            RepositoryResponse<Client> response = await RequestPost<Client>( CLIENT_URI, content );
            return response;
        }
    }
}