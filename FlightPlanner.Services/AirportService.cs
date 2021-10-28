using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDBContext context) : base(context)
        {
        }

        public Airport GetAirport(string search)
        {
            search = search.ToUpper().Trim();
            var airport = _context.Airports.FirstOrDefault(a =>
                a.City.Substring(0, search.Length) == search ||
                a.Country.Substring(0, search.Length) == search ||
                a.AirportCode.Substring(0, search.Length) == search);
            
            return airport;
        }
    }
}
