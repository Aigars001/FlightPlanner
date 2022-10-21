using FlightPlannerCore.Services;
using flightPlannerData;
using Microsoft.AspNetCore.Mvc;

namespace flightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public TestingApiController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightService.Clear();

            return Ok();
        }
    }
}
