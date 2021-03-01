using SampleApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Data.Repository
{
    public interface IApartmentsRepository
    {
        Task AddAsync(Apartment entity);

        Task<Apartment> GetAsync(int Id);

        Task UpdateLocationAsync(int apartmentId, int locationId);

        Task<Apartment> GetByLocationIdAsync(int locationId);
    }
}
