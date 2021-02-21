using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VotingApp.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            Home value = new()
            {
                Title = "Voting App API",
                Message = "Welcome to Voting App API!",
                Instruction = "Let's start by creating your account at [host]/api/Clients."
            };

            return CreatedAtAction( nameof( Get ), new { value.Title }, value );
        }

        private class Home
        {
            public string Title { get; init; }
            public string Message { get; init; }
            public string Instruction { get; init; }
        }
    }
}
