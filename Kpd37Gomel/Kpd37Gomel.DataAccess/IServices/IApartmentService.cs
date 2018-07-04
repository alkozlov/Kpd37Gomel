using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface IApartmentService
    {
        Task<IEnumerable<Apartment>> GetApartmentsAsync();
        Task<Apartment> GetApartmentByIdAsync(Guid apartmentId);
        Task<Apartment> CreateApartmentAsync(Apartment apartment);
        Task<Apartment> UpdateApartmentAsync(Guid apartmentId, Apartment modifiedApartment);
        Task DeleteApartmentAsync(Guid apartmentId);
    }
}