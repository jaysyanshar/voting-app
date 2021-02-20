using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ApiControllerBase<string, Session, SessionDataContext>
    {
        public SessionsController( SessionDataContext context ) : base( context )
        {
        }
    }
}
