using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class VotingItemsController : ApiControllerBase<long, VotingItem, VotingItemDataContext>
    {
        private readonly SessionDataContext _sessionContext;

        public VotingItemsController( VotingItemDataContext context, SessionDataContext sessionContext ) :
            base( context )
        {
            _sessionContext = sessionContext;
        }

        // TODO: A-2
        public override async Task<ActionResult<VotingItem>> Post( VotingItem value )
        {
            string sessionId = Request.Headers[nameof( Session )];
            StatusCodeResult result = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext  );
            if( !( result is OkResult ) )
                return result;

            return await base.Post( value );
        }

        public override async Task<IActionResult> Put( long id, VotingItem value )
        {
            string sessionId = Request.Headers[nameof( Session )];
            StatusCodeResult result = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext );
            if( !( result is OkResult ) )
                return result;

            return await base.Put( id, value );
        }

        public override async Task<IActionResult> Delete( long id )
        {
            string sessionId = Request.Headers[nameof( Session )];
            StatusCodeResult result = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext );
            if( !( result is OkResult ) )
                return result;

            return await base.Delete( id );
        }
    }
}