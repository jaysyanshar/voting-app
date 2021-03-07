using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Core.Repositories
{
    public class CategoryRepository : RepositoryBase, IDataStore<string, Category>
    {
        private const string CATEGORIES_URI = @"Categories";

        public CategoryRepository( HttpClient client ) : base( client )
        {
        }

        public async Task<RepositoryResponse<Category>> AddAsync( Category data )
        {
            KeyValuePair<string, string> sessionHeader = GetSessionHeader();
            HttpContent content = CreateContent( data, sessionHeader );
            RepositoryResponse<Category> response = await RequestPost<Category>( CATEGORIES_URI, content );
            return response;
        }

        public async Task<RepositoryResponse<Category>> GetAsync( string id )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();
            RepositoryResponse<Category> response = await RequestGet<Category>( $"{CATEGORIES_URI}/{id}", headers );
            return response;
        }

        public async Task<RepositoryResponse<IEnumerable<Category>>> GetManyAsync(
            IDictionary<string, string> queryParams = null )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            RepositoryResponse<IEnumerable<Category>> response =
                await RequestGet<IEnumerable<Category>>( CATEGORIES_URI, headers );

            return response;
        }

        public async Task<RepositoryResponse> UpdateAsync( string id, Category data )
        {
            KeyValuePair<string, string> sessionHeader = GetSessionHeader();
            HttpContent content = CreateContent( data, sessionHeader );
            RepositoryResponse response = await RequestUpdate( $"{CATEGORIES_URI}/{id}", content );
            return response;
        }

        public async Task<RepositoryResponse> RemoveAsync( string id )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();
            RepositoryResponse response = await RequestDelete( $"{CATEGORIES_URI}/{id}", headers );
            return response;
        }
    }
}