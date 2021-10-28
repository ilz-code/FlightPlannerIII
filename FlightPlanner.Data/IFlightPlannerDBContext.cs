using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightPlanner.Data
{
    public interface IFlightPlannerDBContext
    {
        DbSet<T> Set<T>() where T : class;

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
