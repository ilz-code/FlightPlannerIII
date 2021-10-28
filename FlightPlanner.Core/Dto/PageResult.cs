using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Dto
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems => Items.Count;
        public List<Flight> Items { get; set; }
    }
}
