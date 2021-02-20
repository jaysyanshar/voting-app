using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ApiControllerBase<string, Admin, AdminDataContext>
    {
        public AdminsController( AdminDataContext context ) : base( context )
        {
        }
    }
}
