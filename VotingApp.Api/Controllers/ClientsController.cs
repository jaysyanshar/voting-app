using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class ClientsController : ApiControllerBase<string, Client, ClientDataContext>
    {
        private readonly SessionDataContext _sessionContext;

        // TODO: B-2
        public ClientsController( ClientDataContext context, SessionDataContext sessionContext ) : base( context )
        {
            _sessionContext = sessionContext;
        }

        public override async Task<IActionResult> Put( string id, Client value )
        {
            string sessionId = Request.Headers[nameof( Session )];
            if( string.IsNullOrWhiteSpace( sessionId ) )
                return Unauthorized();

            StatusCodeResult sessionValid = await ValidateSession( sessionId, UserRole.Type.Client, _sessionContext );
            if( !( sessionValid is OkResult ) )
                return sessionValid;

            return await base.Put( id, value );
        }

        public override async Task<IActionResult> Delete( string id )
        {
            string sessionId = Request.Headers[nameof( Session )];
            if( string.IsNullOrWhiteSpace( sessionId ) )
                return Unauthorized();

            StatusCodeResult sessionValid = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext );
            if( !( sessionValid is OkResult ) )
                return sessionValid;

            return await base.Delete( id );
        }
    }
}