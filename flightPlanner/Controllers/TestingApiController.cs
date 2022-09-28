using flightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace flightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            FlightStorage.Clear();
            return Ok();
        }
    }
}
