using System.Net;

namespace VotingApp.Core.Models
{
    public class RepositoryResponse<T> : RepositoryResponse
    {
        public T Content { get; set; }
    }

    public class RepositoryResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
    }
}