using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Services
{
    public abstract class RepositoryBase
    {
        private static bool _isClientInitialized;

        protected readonly HttpClient Client;

        protected RepositoryBase( HttpClient client )
        {
            Client = client;

            if( _isClientInitialized )
                return;

            Client.BaseAddress = new Uri( @"https://localhost:44303/api/" );
            Client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
            _isClientInitialized = true;
        }

        protected virtual HttpContent CreateContent<TContent>( TContent body,
            params KeyValuePair<string, string>[] headers ) where TContent : class
        {
            HttpContent content = new StringContent( JsonHelper.Serialize( body ) );
            content.Headers.ContentType = new MediaTypeHeaderValue( "application/json" );

            foreach( KeyValuePair<string, string> header in headers )
            {
                content.Headers.Add( header.Key, header.Value );
            }

            return content;
        }

        protected KeyValuePair<string, string> GetSessionHeader()
        {
            Session session = DependencyService.Resolve<Session>();
            return session == null ? default : new KeyValuePair<string, string>( nameof( Session ), session.Id );
        }

        protected virtual async Task<RepositoryResponse<TReturn>> RequestGet<TReturn>( string uri,
            IDictionary<string, string> headers = null )
            where TReturn : class
        {
            HttpStatusCode code = default;
            HttpRequestMessage request = CreateRequestMessage( CreateRelativeUri( uri ), HttpMethod.Get, headers );
            try
            {
                HttpResponseMessage response = await Client.SendAsync( request );
                code = response.StatusCode;

                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();

                return new RepositoryResponse<TReturn>
                {
                    StatusCode = code,
                    Success = true,
                    Content = JsonHelper.Deserialize<TReturn>( body )
                };
            }
            catch
            {
                return new RepositoryResponse<TReturn>
                {
                    StatusCode = code,
                    Success = false,
                    Content = null
                };
            }
        }

        protected virtual async Task<RepositoryResponse<TReturn>> RequestPost<TReturn>( string uri,
            HttpContent content ) where TReturn : class
        {
            HttpStatusCode code = default;
            try
            {
                HttpResponseMessage response = await Client.PostAsync( uri, content );
                code = response.StatusCode;

                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return new RepositoryResponse<TReturn>
                {
                    StatusCode = code,
                    Success = true,
                    Content = JsonHelper.Deserialize<TReturn>( body )
                };
            }
            catch
            {
                return new RepositoryResponse<TReturn>
                {
                    StatusCode = code,
                    Success = false,
                    Content = null
                };
            }
        }

        protected virtual async Task<RepositoryResponse> RequestUpdate( string uri, HttpContent content )
        {
            HttpStatusCode code = default;
            try
            {
                HttpResponseMessage response = await Client.PutAsync( uri, content );
                code = response.StatusCode;

                response.EnsureSuccessStatusCode();
                return new RepositoryResponse
                {
                    StatusCode = code,
                    Success = true
                };
            }
            catch
            {
                return new RepositoryResponse
                {
                    StatusCode = code,
                    Success = false
                };
            }
        }

        protected virtual async Task<RepositoryResponse> RequestDelete( string uri,
            IDictionary<string, string> headers )
        {
            HttpStatusCode code = default;
            HttpRequestMessage request =
                CreateRequestMessage( CreateRelativeUri( uri ), HttpMethod.Delete, headers );

            try
            {
                HttpResponseMessage response = await Client.SendAsync( request );
                code = response.StatusCode;

                response.EnsureSuccessStatusCode();
                return new RepositoryResponse
                {
                    StatusCode = code,
                    Success = true
                };
            }
            catch
            {
                return new RepositoryResponse
                {
                    StatusCode = code,
                    Success = false
                };
            }
        }

        private static HttpRequestMessage CreateRequestMessage( Uri requestUri, HttpMethod method,
            IDictionary<string, string> headers = null,
            HttpContent content = null )
        {
            HttpRequestMessage request = new()
            {
                Content = content,
                Method = method,
                RequestUri = requestUri
            };

            if( headers == null )
                return request;

            foreach( KeyValuePair<string, string> header in headers )
            {
                request.Headers.Add( header.Key, header.Value );
            }

            return request;
        }

        private Uri CreateRelativeUri( string uri )
        {
            return new( Client.BaseAddress, uri );
        }
    }
}