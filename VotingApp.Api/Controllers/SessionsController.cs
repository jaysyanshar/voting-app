using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;
using VotingApp.Api.Utils.Helpers;

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

        [HttpPost( nameof( Login ) )]
        public async Task<ActionResult<Session>> Post( Login value )
        {
            if( !value.ValidateFields() )
                return ValidationProblem();

            User user;
            if( value.UserRole.ParseEnum<UserRole.Type>() == UserRole.Type.Admin )
                user = await _adminContext.DataSet.FindAsync( value.Email );
            else
                user = await _clientContext.DataSet.FindAsync( value.Email );

            if( !user.Password.Equals( value.Password ) )
                return Unauthorized();

            Session session = new()
            {
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString(),
                UserEmail = value.Email,
                UserRole = value.UserRole
            };

            return await base.Post( session );
        }
    }
}