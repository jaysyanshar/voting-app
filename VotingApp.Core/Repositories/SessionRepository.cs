using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Services;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Core.Repositories
{
    public class SessionRepository : RepositoryBase
    {
        private const string LOGIN_URI = @"Sessions/Login";
        private const string LOGOUT_URI = @"Sessions/Logout";

        public SessionRepository( HttpClient client ) : base( client )
        {
        }

        public async Task<RepositoryResponse<Session>> Login( string email, string password, UserRole.Type role )
        {
            if( !ValidationHelper.ValidateEmail( email ) || !ValidationHelper.ValidatePassword( password ) )
                return null;

            Login login = new Login
            {
                Email = email,
                Password = password,
                UserRole = role.ToEnumString()
            };

            HttpContent content = CreateContent( login );
            RepositoryResponse<Session> result = await RequestPost<Session>( LOGIN_URI, content );
            if( !result.Success )
                return result;

            if( DependencyService.Resolve<Session>() == null )
                DependencyService.RegisterSingleton<Session, Session>( result.Content );

            return result;
        }

        public async Task<RepositoryResponse> Logout()
        {
            KeyValuePair<string, string> sessionHeader = GetSessionHeader();
            HttpContent content = CreateContent<Session>( null, sessionHeader );
            RepositoryResponse logout = await RequestUpdate( LOGOUT_URI, content );
            if( logout.Success )
                DependencyService.Remove<Session>();

            return logout;
        }
    }
}