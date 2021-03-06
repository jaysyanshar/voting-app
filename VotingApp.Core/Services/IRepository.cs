using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingApp.Core.Models;

namespace VotingApp.Core.Services
{
    public interface IRepository<in TKey, TValue> where TValue : class
    {
        Task<RepositoryResponse<TValue>> AddAsync( TValue data );

        Task<RepositoryResponse<TValue>> GetAsync( TKey id );

        Task<RepositoryResponse<IEnumerable<TValue>>> GetManyAsync( IDictionary<string, string> queryParams = null );

        Task<RepositoryResponse> UpdateAsync( TKey id, TValue data );

        Task<RepositoryResponse> RemoveAsync( TKey id );
    }
}