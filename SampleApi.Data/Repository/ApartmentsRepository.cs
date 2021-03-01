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
    public class ApartmentsRepository : IApartmentsRepository
    {
        protected readonly SampleApiContext _context;

        public ApartmentsRepository(DbContextOptions<SampleApiContext> options)
            : this(new SampleApiContext(options))
        {

        }

        ApartmentsRepository(SampleApiContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Apartment entity)
        {
            _context.Set<Apartment>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Apartment> GetAsync(int Id)
        {
            var apartment = await _context.Set<Apartment>().FindAsync(Id);
            return apartment;
        }

        public async Task UpdateLocationAsync(int apartmentId, int locationId)
        {
            var apartment = await _context.Set<Apartment>().FindAsync(apartmentId);
            if (apartment != null)
                apartment.LocationId = locationId;
            await _context.SaveChangesAsync();
        }

        public async Task<Apartment> GetByLocationIdAsync(int locationId)
        {
            var location = await _context.Set<Apartment>().Where(apt => apt.LocationId == locationId).FirstOrDefaultAsync();
            return location;
        }
    }
}
