using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class CategoriesController : ApiControllerBase<int, Category, CategoryDataContext>
    {
        private readonly SessionDataContext _sessionContext;

        public CategoriesController( CategoryDataContext context, SessionDataContext sessionContext ) : base( context )
        {
            _sessionContext = sessionContext;
        }

        // TODO: A-6
        public override async Task<ActionResult<IEnumerable<Category>>> GetMany()
        {
            StatusCodeResult result = await SessionCheck();
            if( !( result is OkResult ) )
                return result;

            return await base.GetMany();
        }

        public override async Task<ActionResult<Category>> Get( int id )
        {
            StatusCodeResult result = await SessionCheck();
            if( !( result is OkResult ) )
                return result;

            return await base.Get( id );
        }

        public override async Task<ActionResult<Category>> Post( Category value )
        {
            StatusCodeResult result = await SessionCheck();
            if( !( result is OkResult ) )
                return result;

            return await base.Post( value );
        }

        public override async Task<IActionResult> Put( int id, Category value )
        {
            StatusCodeResult result = await SessionCheck();
            if( !( result is OkResult ) )
                return result;

            return await base.Put( id, value );
        }

        public override async Task<IActionResult> Delete( int id )
        {
            StatusCodeResult result = await SessionCheck();
            if( !( result is OkResult ) )
                return result;

            return await base.Delete( id );
        }

        private async Task<StatusCodeResult> SessionCheck()
        {
            string sessionId = Request.Headers[nameof( Session )];
            if( string.IsNullOrWhiteSpace( sessionId ) )
                return BadRequest();

            StatusCodeResult result = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext );
            return result;
        }
    }
}