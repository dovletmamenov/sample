using SampleApi.Data.Entities;
using SampleApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Tests
{
    public class FakeLocationsRepository : ILocationsRepository
    {
        public List<Location> Entities { get; set; }

        public FakeLocationsRepository()
        {
            Entities = new List<Location>();
        }

        public Task AddAsync(Location entity)
        {
            Entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Location> GetAsync(int Id)
        {
            var location = Entities.Find(l => l.Id == Id);
            return Task.FromResult(location);
        }

        public Task<Location> GetByAsync(string streetName, string building, string flat)
        {
            var location =  Entities.Find(l => l.StreetName == streetName && l.Building == building && l.Flat == flat);
            return Task.FromResult(location);
        }
    }
}
