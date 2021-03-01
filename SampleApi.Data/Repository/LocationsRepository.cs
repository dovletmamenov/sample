using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SampleApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Data.Repository
{
    public class LocationsRepository : ILocationsRepository
    {
        protected readonly SampleApiContext _context;

        public LocationsRepository(DbContextOptions<SampleApiContext> options)
            : this(new SampleApiContext(options))
        {

        }

        LocationsRepository(SampleApiContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Location entity)
        {
            _context.Set<Location>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Location> GetAsync(int Id)
        {
            var location = await _context.Set<Location>().FindAsync(Id);
            return location;
        }

        public async Task<Location> GetByAsync(string streetName, string building, string flat)
        {
            var location = await _context.Set<Location>()
                .Where(loc => loc.StreetName == streetName && loc.Building == building && loc.Flat == flat)
                .FirstOrDefaultAsync();
            return location;
        }
    }
}
