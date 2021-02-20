using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet( "Page" )]
        public async Task<ActionResult<IEnumerable<VotingItem>>> GetManyByPage( [FromQuery] int index = 0,
            [FromQuery] int count = 20 )
        {
            if( index < 0 )
                return BadRequest();

            if( count <= 0 )
                return BadRequest();

            // TODO: solve bugs on LINQ expression
            return await Context.DataSet.Where( (val, i) => i >= index && i < count ).ToListAsync();
        }

        [HttpGet( "Search" )]
        public async Task<ActionResult<IEnumerable<VotingItem>>> GetManyBySearch( [FromQuery] string name )
        {
            if( string.IsNullOrEmpty( name ) )
                name = string.Empty;

            return await Context.DataSet.Where(  val => val.Name.ToLower().Contains( name.ToLower() ) )
                .ToListAsync();
        }

        [HttpGet( "Filter" )]
        public async Task<ActionResult<IEnumerable<VotingItem>>> GetManyByFilter( [FromQuery] string category )
        {
            if( string.IsNullOrEmpty( category ) )
                category = string.Empty;

            return await Context.DataSet.Where( val => val.Categories.ToLower().Contains( category.ToLower() ) )
                .ToListAsync();
        }
    }
}