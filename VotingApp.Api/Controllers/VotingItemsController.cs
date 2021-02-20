using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotingItemsController : ApiControllerBase<long, VotingItem, VotingItemDataContext>
    {
        public VotingItemsController( VotingItemDataContext context ) : base( context )
        {
        }
    }
}
