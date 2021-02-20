using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;
using VotingApp.Api.Utils.Helpers;

namespace VotingApp.Api.Controllers
{
    public abstract class ApiControllerBase<TKey, TValue, TContext> : ControllerBase
        where TValue : class, IModel<TKey>
        where TContext : DataContextBase<TValue, TContext>
    {
        protected ApiControllerBase( TContext context )
        {
            Context = context;
        }

        protected TContext Context { get; }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TValue>>> GetMany()
        {
            return await Context.DataSet.ToListAsync();
        }

        // GET: api/[controller]/{id}
        [HttpGet( "{id}" )]
        public virtual async Task<ActionResult<TValue>> Get( TKey id )
        {
            TValue value = await Context.DataSet.FindAsync( id );

            if( value == null )
                return NotFound();

            return value;
        }

        // POST: api/[controller]
        [HttpPost]
        public virtual async Task<ActionResult<TValue>> Post( TValue value )
        {
            if( !value.ValidateFields() )
                return ValidationProblem();

            TValue existingData = await Context.DataSet.FindAsync( value.GetKey() );
            if( existingData != null )
                return Conflict();

            await Context.DataSet.AddAsync( value );
            await Context.SaveChangesAsync();

            return CreatedAtAction( nameof( Get ), new { id = value.GetKey() }, value );
        }

        // PUT: api/[controller]/{id}
        [HttpPut( "{id}" )]
        public virtual async Task<IActionResult> Put( TKey id, TValue value )
        {
            if( !EqualityComparer<TKey>.Default.Equals( id, value.GetKey() ) )
                return BadRequest();

            if( !value.ValidateFields() )
                return ValidationProblem();

            Context.Entry( value ).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch( DbUpdateConcurrencyException ) when( !IsExists( id ) )
            {
                return NotFound();
            }

            return NoContent();
        }


        // DELETE: api/[controller]/{id}
        [HttpDelete( "{id}" )]
        public virtual async Task<IActionResult> Delete( TKey id )
        {
            TValue value = await Context.DataSet.FindAsync( id );
            if( value == null )
                return NotFound();

            Context.DataSet.Remove( value );
            await Context.SaveChangesAsync();

            return NoContent();
        }

        protected virtual bool IsExists( TKey id )
        {
            return Context.DataSet.Any( value => EqualityComparer<TKey>.Default.Equals( id, value.GetKey() ) );
        }

        protected async Task<StatusCodeResult> ValidateSession( string sessionId,
            UserRole.Type expectedRole, SessionDataContext sessionContext )
        {
            if( string.IsNullOrWhiteSpace( sessionId ) )
                return BadRequest();

            Session session = await sessionContext.DataSet.FindAsync( sessionId );

            if( session == null )
                return NotFound();

            if( !session.LoggedIn )
                return Unauthorized();

            try
            {
                UserRole.Type actualRole = session.UserRole.ParseEnum<UserRole.Type>();
                if( actualRole != expectedRole )
                    return Unauthorized();
            }
            catch
            {
                return UnprocessableEntity();
            }

            return Ok();
        }
    }
}