using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Services;

namespace VotingApp.Admin.Repositories
{
    public class CategoryRepository : RepositoryBase, IRepository<string, Category>
    {
        private const string CATEGORIES_URI = @"Categories";

        public CategoryRepository( HttpClient client ) : base( client )
        {
        }

        public Task<RepositoryResponse<Category>> AddAsync( Category data )
        {
            throw new System.NotImplementedException();
        }

        public Task<RepositoryResponse<Category>> GetAsync( string id )
        {
            throw new System.NotImplementedException();
        }

        public async Task<RepositoryResponse<IEnumerable<Category>>> GetManyAsync(
            IDictionary<string, string> queryParams = null )
        {
            IDictionary<string, string> headers = GetSessionHeaderInDictionary();

            RepositoryResponse<IEnumerable<Category>> response =
                await RequestGet<IEnumerable<Category>>( CATEGORIES_URI, headers );

            return response;
        }

        public Task<RepositoryResponse> UpdateAsync( string id, Category data )
        {
            throw new System.NotImplementedException();
        }

        public Task<RepositoryResponse> RemoveAsync( string id )
        {
            throw new System.NotImplementedException();
        }
    }
}