using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightServiceSearch
    {
        Flight GetFlightBySearch(FlightSearch fs);
    }
}
