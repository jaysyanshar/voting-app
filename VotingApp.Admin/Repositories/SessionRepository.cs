using System.Net.Http;
using System.Threading.Tasks;
using VotingApp.Core.Models;
using VotingApp.Core.Services;
using VotingApp.Core.Utils.Helpers;

namespace VotingApp.Admin.Repositories
{
    public class SessionRepository : RepositoryBase
    {
        private const string LOGIN_URI = @"Sessions/Login";
        private const string LOGOUT_URI = @"Sessions/Logout";

        public SessionRepository( HttpClient client ) : base( client )
        {
        }

        public async Task<Session> Login( string email, string password, UserRole.Type role )
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
            Session result = await RequestPost<Session>( LOGIN_URI, content );
            if( result == null )
                return null;

            Client.DefaultRequestHeaders.Add( nameof( Session ), result.Id );
            return result;
        }

        public async Task<bool> Logout()
        {
            bool logout = await RequestDelete( LOGOUT_URI );
            if( logout )
                Client.DefaultRequestHeaders.Remove( nameof( Session ) );

            return logout;
        }
    }
}