using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Core.Models;
using Category = VotingApp.Api.Models.Category;
using Session = VotingApp.Api.Models.Session;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class CategoriesController : ApiControllerBase<string, Category, CategoryDataContext>
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

        public override async Task<ActionResult<Category>> Get( string id )
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

        public override async Task<IActionResult> Put( string id, Category value )
        {
            StatusCodeResult result = await SessionCheck();
            if( !( result is OkResult ) )
                return result;

            return await base.Put( id, value );
        }

        public override async Task<IActionResult> Delete( string id )
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