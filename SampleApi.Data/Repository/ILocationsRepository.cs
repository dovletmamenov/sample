using SampleApi.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.Data.Repository
{
    public interface ILocationsRepository
    {
        Task AddAsync(Location entity);

        Task<Location> GetAsync(int Id);

        Task<Location> GetByAsync(string streetName, string building, string flat);
    }
}
