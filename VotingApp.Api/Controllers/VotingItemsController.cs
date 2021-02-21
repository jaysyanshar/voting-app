using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingApp.Api.DataContexts;
using VotingApp.Core.Models;
using Category = VotingApp.Api.Models.Category;
using Session = VotingApp.Api.Models.Session;
using VotingItem = VotingApp.Api.Models.VotingItem;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class VotingItemsController : ApiControllerBase<string, VotingItem, VotingItemDataContext>
    {
        private readonly SessionDataContext _sessionContext;
        private readonly CategoryDataContext _categoryContext;

        public VotingItemsController( VotingItemDataContext context, SessionDataContext sessionContext,
            CategoryDataContext categoryContext ) :
            base( context )
        {
            _sessionContext = sessionContext;
            _categoryContext = categoryContext;
        }

        // TODO: A-2
        public override async Task<ActionResult<VotingItem>> Post( VotingItem value )
        {
            string sessionId = Request.Headers[nameof( Session )];
            StatusCodeResult sessionValid = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext  );
            if( !( sessionValid is OkResult ) )
                return sessionValid;

            StatusCodeResult categoryValid = await CategoryCheck( value );
            if( !( categoryValid is OkResult ) )
                return categoryValid;

            value.CreatedDate = DateTime.Now;
            
            return await base.Post( value );
        }

        public override async Task<IActionResult> Put( string id, VotingItem value )
        {
            string sessionId = Request.Headers[nameof( Session )];
            StatusCodeResult result = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext );
            if( !( result is OkResult ) )
                return result;

            VotingItem existingData = await Context.DataSet.FindAsync( id );
            if( value.CreatedDate != existingData.CreatedDate )
                return BadRequest();

            return await base.Put( id, value );
        }

        public override async Task<IActionResult> Delete( string id )
        {
            string sessionId = Request.Headers[nameof( Session )];
            StatusCodeResult result = await ValidateSession( sessionId, UserRole.Type.Admin, _sessionContext );
            if( !( result is OkResult ) )
                return result;

            return await base.Delete( id );
        }

        // TODO: B-6
        // TODO: B-7
        // TODO: A-3
        [HttpGet( "Page" )]
        public async Task<ActionResult<IEnumerable<VotingItem>>> GetManyByPage( [FromQuery] int skip = 0,
            [FromQuery] int take = 20 )
        {
            if( skip < 0 || take <= 0 )
                return BadRequest();

            return await Context.DataSet.Skip( skip ).Take( take ).ToListAsync();
        }

        // TODO: A-4
        [HttpGet( "Search" )]
        public async Task<ActionResult<IEnumerable<VotingItem>>> GetManyBySearch( [FromQuery] string name )
        {
            if( string.IsNullOrEmpty( name ) )
                name = string.Empty;

            return await Context.DataSet.Where(  val => val.Name.ToLower().Contains( name.ToLower() ) )
                .ToListAsync();
        }

        // TODO: A-5
        [HttpGet( "Filter" )]
        public async Task<ActionResult<IEnumerable<VotingItem>>> GetManyByFilter( [FromQuery] string category )
        {
            if( string.IsNullOrEmpty( category ) )
                category = string.Empty;

            return await Context.DataSet.Where( val => val.Categories.ToLower().Contains( category.ToLower() ) )
                .ToListAsync();
        }

        private async Task<StatusCodeResult> CategoryCheck( VotingItem value )
        {
            Category category =
                await _categoryContext.DataSet.FirstOrDefaultAsync(
                    c => c.Name.ToLower().Equals( value.Categories.ToLower() ) );

            if( category == null )
                return NotFound();

            return Ok();
        }
    }
}