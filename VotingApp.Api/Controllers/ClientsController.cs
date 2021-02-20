using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiControllerBase<string, Client, ClientDataContext>
    {
        public ClientsController( ClientDataContext context ) : base( context )
        {
        }
    }
}
