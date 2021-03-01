using Microsoft.EntityFrameworkCore.ChangeTracking;
using SampleApi.Data.Entities;
using SampleApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Tests
{
    public class FakeApartmentsRepository : IApartmentsRepository
    {
        public List<Apartment> Apartments { get; set; }

        public FakeApartmentsRepository()
        {
            Apartments = new List<Apartment>();
        }
        public Task AddAsync(Apartment entity)
        {
            Apartments.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Apartment> GetAsync(int Id)
        {
            var apt = Apartments.Find(l => l.Id == Id);
            return Task.FromResult(apt);
        }

        public Task<Apartment> GetByLocationIdAsync(int locationId)
        {
            var apt =  Apartments.Find(l => l.LocationId == locationId);
            return Task.FromResult(apt);
        }

        public Task UpdateLocationAsync(int apartmentId, int locationId)
        {
            var apt = Apartments.Find(l => l.Id == apartmentId);
            apt.LocationId = locationId;
            return Task.CompletedTask;
        }
    }
}
