using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Services
{
    public abstract class RepositoryBase
    {
        protected readonly HttpClient Client;

        protected RepositoryBase( HttpClient client )
        {
            Client = client;
            Client.BaseAddress = new Uri( @"https://localhost:44303/api/" );
            Client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
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

        protected virtual async Task<TReturn> RequestGet<TReturn>( string uri ) where TReturn : class
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync( uri );
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return JsonHelper.Deserialize<TReturn>( body );
            }
            catch
            {
                return null;
            }
        }

        protected virtual async Task<TReturn> RequestPost<TReturn>( string uri, HttpContent content ) where TReturn : class
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsync( uri, content );
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                return JsonHelper.Deserialize<TReturn>( body );
            }
            catch
            {
                return null;
            }
        }

        protected virtual async Task<bool> RequestUpdate( string uri, HttpContent content )
        {
            try
            {
                HttpResponseMessage response = await Client.PutAsync( uri, content );
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual async Task<bool> RequestDelete( string uri )
        {
            try
            {
                HttpResponseMessage response = await Client.DeleteAsync( uri );
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}