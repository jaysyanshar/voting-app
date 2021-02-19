using Microsoft.AspNetCore.Mvc;
using VotingApp.Api.DataContexts;
using VotingApp.Api.Models;

namespace VotingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ApiControllerBase<long, Category, CategoryDataContext>
    {
        public CategoriesController( CategoryDataContext context ) : base( context )
        {
        }
    }
}
