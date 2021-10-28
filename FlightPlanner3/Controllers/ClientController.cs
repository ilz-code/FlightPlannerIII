using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner3.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        private readonly IFlightServiceSearch _flightServiceSearch;
        private readonly IFlightService _flightService;

        public ClientController(IAirportService airportService, IMapper mapper, IFlightServiceSearch flightServiceSearch, IFlightService flightService)
        {
            _airportService = airportService;
            _mapper = mapper;
            _flightServiceSearch = flightServiceSearch;
            _flightService = flightService;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            AirportResponse[] airports = new AirportResponse[1];
            var airport = _airportService.GetAirport(search);
            airports[0] = _mapper.Map<AirportResponse>(airport);
            return Ok(airports);
        }


        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(FlightSearch fs)
        {
            if (fs.From == fs.To)
                return BadRequest();

            PageResult result = new PageResult();
            result.Items = new List<Flight>();

            var flight = _flightServiceSearch.GetFlightBySearch(fs);

            if (flight != null)
                result.Items.Add(flight);

            result.Page = result.TotalItems;

            return Ok(result);
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
    }
}
