using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Core.Models;
using VotingApp.Core.Utils.Helpers;
using Login = VotingApp.Api.Models.Login;
using Session = VotingApp.Api.Models.Session;
using User = VotingApp.Core.Models.User;

namespace VotingApp.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class SessionsController : ApiControllerBase<string, Session, SessionDataContext>
    {
        private readonly AdminDataContext _adminContext;
        private readonly ClientDataContext _clientContext;

        public SessionsController( SessionDataContext context, AdminDataContext adminContext,
            ClientDataContext clientContext ) : base( context )
        {
            _adminContext = adminContext;
            _clientContext = clientContext;
        }

        // TODO: A-1
        // TODO: B-1
        [HttpPost( nameof( Login ) )]
        public async Task<ActionResult<Session>> Post( Login value )
        {
            if( !value.ValidateFields() )
                return ValidationProblem();

            User user;
            if( value.UserRole.ParseEnum<UserRole.Type>() == UserRole.Type.Admin )
                user = await _adminContext.DataSet.FindAsync( value.Email );
            else if( value.UserRole.ParseEnum<UserRole.Type>() == UserRole.Type.Client )
                user = await _clientContext.DataSet.FindAsync( value.Email );
            else
                user = null;

            if( user == null || !user.Password.Equals( value.Password ) )
                return Unauthorized();

            Session session = new()
            {
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString(),
                UserEmail = value.Email,
                UserRole = value.UserRole,
                LoggedIn = true
            };

            return await base.Post( session );
        }

        [HttpPut( "Logout" )]
        public async Task<IActionResult> Put()
        {
            string id = Request.Headers[nameof( Session )];
            Session session = await Context.DataSet.FindAsync( id );
            if( session == null )
                return BadRequest();

            session.LoggedIn = false;
            return await base.Put( id, session );
        }

        public override async Task<IActionResult> Put( string id, Session value ) => NotFound();

        public override async Task<ActionResult<IEnumerable<Session>>> GetMany() => NotFound();

        public override async Task<ActionResult<Session>> Get( string id ) => NotFound();

        public override async Task<ActionResult<Session>> Post( Session value ) => NotFound();

        public override async Task<IActionResult> Delete( string id ) => NotFound();
    }
}