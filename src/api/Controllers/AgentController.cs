using infrastructure.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController(IAgent agent) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Start(string userInput)
        {
           var response= await agent.Start(userInput);
            return Ok(response.Response);
        }
    }
}
