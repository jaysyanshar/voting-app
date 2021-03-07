using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Core.Repositories
{
    public class VoteRepository : RepositoryBase
    {
        private const string VOTE_RECORDS_URI = @"VoteRecords";
        private const string VOTE_URI = @"VoteRecords/Vote";

        public VoteRepository( HttpClient client ) : base( client )
        {
        }

        public async Task<RepositoryResponse<VoteRecord>> Vote( string itemId )
        {
            KeyValuePair<string, string> headers = GetSessionHeader();
            HttpContent content = CreateContent<VoteRecord>( null, headers );
            RepositoryResponse<VoteRecord> response = await RequestPost<VoteRecord>( $"{VOTE_URI}/{itemId}", content );
            return response;
        }

        public async Task<RepositoryResponse<bool>> CanVote( string email, string itemId )
        {
            RepositoryResponse<VoteRecord> response = await RequestGet<VoteRecord>( $"{VOTE_RECORDS_URI}/{email}/{itemId}" );
            return new RepositoryResponse<bool>
            {
                StatusCode = response.StatusCode,
                Success = response.Success,
                Content = response.Content == null
            };
        }
    }
}