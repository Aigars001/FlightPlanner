using FlightPlannerCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightPlannerData
{
    public interface IFlightPlannerDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        DbSet<Flight> Flights { get; set; }
        DbSet<Airport> Airports { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
