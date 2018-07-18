using System;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;

namespace Kpd37Gomel.DataAccess.IServices
{
    public interface IApartmentService
    {
        Task<IQueryable<Apartment>> GetApartmentsAsync(bool includeDeleted = false);
        Task<Apartment> GetApartmentByIdAsync(Guid apartmentId);
        Task<Apartment> CreateApartmentAsync(Apartment apartment);
        Task<Apartment> UpdateApartmentAsync(Guid apartmentId, Apartment modifiedApartment);
        Task DeleteApartmentAsync(Guid apartmentId);
    }
}