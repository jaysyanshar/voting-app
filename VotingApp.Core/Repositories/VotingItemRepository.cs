using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Core.Repositories
{
    public class VotingItemRepository : RepositoryBase, IDataStore<string, VotingItem>
    {
        private const string VOTING_ITEMS_URI = @"VotingItems";
        private const string VOTING_ITEMS_PAGE_URI = @"VotingItems/Page";
        private const string VOTING_ITEMS_SEARCH_URI = @"VotingItems/Search";
        private const string VOTING_ITEMS_FILTER_URI = @"VotingItems/Filter";

        public const string SKIP_QUERY = "skip";
        public const string TAKE_QUERY = "take";
        private const string SEARCH_QUERY = "name";
        private const string CATEGORY_QUERY = "category";

        public VotingItemRepository( HttpClient client ) : base( client )
        {
        }

        public async Task<RepositoryResponse<VotingItem>> AddAsync( VotingItem data )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();
            HttpContent content = CreateContent( data, headers.ToArray() );

            RepositoryResponse<VotingItem> response = await RequestPost<VotingItem>( VOTING_ITEMS_URI, content );
            return response;
        }

        public async Task<RepositoryResponse<VotingItem>> GetAsync( string id )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            RepositoryResponse<VotingItem> response =
                await RequestGet<VotingItem>( $"{VOTING_ITEMS_URI}/{id}", headers );

            return response;
        }

        public async Task<RepositoryResponse<IEnumerable<VotingItem>>> GetManyAsync( IDictionary<string, string> queryParams = null )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            string query = string.Empty;
            if( queryParams != null )
            {
                query = queryParams.Aggregate( query, ( current, param ) => current + $"{param.Key}={param.Value}&" );
            }

            RepositoryResponse<IEnumerable<VotingItem>> response =
                await RequestGet<IEnumerable<VotingItem>>( $"{VOTING_ITEMS_PAGE_URI}?{query}", headers );

            return response;
        }

        public async Task<RepositoryResponse<IEnumerable<VotingItem>>> SearchAsync( string searchQuery )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            RepositoryResponse<IEnumerable<VotingItem>> response =
                await RequestGet<IEnumerable<VotingItem>>( $"{VOTING_ITEMS_SEARCH_URI}?{SEARCH_QUERY}={searchQuery}", headers );

            return response;
        }

        public async Task<RepositoryResponse<IEnumerable<VotingItem>>> FilterAsync( string filterQuery )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            RepositoryResponse<IEnumerable<VotingItem>> response =
                await RequestGet<IEnumerable<VotingItem>>( $"{VOTING_ITEMS_FILTER_URI}?{CATEGORY_QUERY}={filterQuery}", headers );

            return response;
        }

        public async Task<RepositoryResponse> UpdateAsync( string id, VotingItem data )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();
            HttpContent content = CreateContent( data, headers.ToArray() );

            RepositoryResponse response = await RequestUpdate( $"{VOTING_ITEMS_URI}/{id}", content );
            return response;
        }

        public async Task<RepositoryResponse> RemoveAsync( string id )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            RepositoryResponse response = await RequestDelete( $"{VOTING_ITEMS_URI}/{id}", headers );
            return response;
        }
    }
}