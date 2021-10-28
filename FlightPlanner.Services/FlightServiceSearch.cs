using System.Linq;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightServiceSearch : IFlightServiceSearch
    {
        protected readonly IFlightPlannerDBContext _context;

        public FlightServiceSearch(IFlightPlannerDBContext context)
        {
            _context = context;
        }

        public Flight GetFlightBySearch(FlightSearch fs)
        {
            return _context.Flights.Include(f => f.From).Include(f => f.To).FirstOrDefault(f =>
                f.From.AirportCode == fs.From
                && f.To.AirportCode == fs.To);
        }
    }
}
