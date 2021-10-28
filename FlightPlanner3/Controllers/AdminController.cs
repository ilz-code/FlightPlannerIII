using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlightPlanner3.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator> _validators;
        private static readonly object flightsLock = new object();

        public AdminController(IFlightService flightService, IMapper mapper, IEnumerable<IValidator> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight == null)
                return NotFound();
            return Ok(_mapper.Map<FlightResponse>(flight));
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest request)
        {

            if (!_validators.All(s => s.IsValid(request)))
                return BadRequest();
            
            var flight = _mapper.Map<Flight>(request);

            if (_flightService.Exists(flight))
                return Conflict();

            _flightService.Create(flight);
            return Created("", _mapper.Map<FlightResponse>(flight));
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            lock (flightsLock)
            {
                var flight = _flightService.GetFullFlightById(id);
                _flightService.Delete(flight);
                return Ok();
            }
        }
    }
}
