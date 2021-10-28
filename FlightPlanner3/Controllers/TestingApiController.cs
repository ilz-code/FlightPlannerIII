using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner3.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly IDbServiceExtended _service;

        public TestingApiController(IDbServiceExtended service)
        {
            _service = service;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            _service.DeleteAll<Flight>();
            _service.DeleteAll<Airport>();
            
            return Ok();
        } 
    }
}
