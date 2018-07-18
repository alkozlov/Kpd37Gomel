using System;
using System.Linq;
using System.Threading.Tasks;
using Kpd37Gomel.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Kpd37Gomel.DataAccess.IServices.Implementation
{
    public class ApartmentService : BaseService, IApartmentService
    {
        public ApartmentService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IQueryable<Apartment>> GetApartmentsAsync(bool includeDeleted = false)
        {
            var apartments = await this.Context.Apartments
                .Include(i => i.Tenants)
                .Where(x => x.IsDeleted == includeDeleted)
                .ToListAsync();

            return apartments.AsQueryable();
        }

        public async Task<Apartment> GetApartmentByIdAsync(Guid apartmentId)
        {
            var apartment = await this.Context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == apartmentId);

            return apartment;
        }

        public async Task<Apartment> CreateApartmentAsync(Apartment apartment)
        {
            this.Context.Apartments.Add(apartment);
            await this.Context.SaveChangesAsync();

            return apartment;
        }

        public async Task<Apartment> UpdateApartmentAsync(Guid apartmentId, Apartment modifiedApartment)
        {
            var apartment = await this.Context.Apartments.FirstOrDefaultAsync(x => x.Id == apartmentId);
            if (apartment == null) throw new Exception("Apartment not found.");

            apartment.ApartmentNumber = modifiedApartment.ApartmentNumber;
            apartment.FloorNumber = modifiedApartment.FloorNumber;
            apartment.TotalAreaSnb = modifiedApartment.TotalAreaSnb;
            apartment.TotalArea = modifiedApartment.TotalArea;
            apartment.LivingSpace = modifiedApartment.LivingSpace;
            apartment.VoteRate = modifiedApartment.VoteRate;
            apartment.RoomsCount = modifiedApartment.RoomsCount;

            this.Context.Apartments.Update(apartment);
            await this.Context.SaveChangesAsync();

            return apartment;

        }

        public async Task DeleteApartmentAsync(Guid apartmentId)
        {
            var apartment = await this.Context.Apartments.FirstOrDefaultAsync(x => x.Id == apartmentId);

            if (apartment == null || apartment.IsDeleted)
            {
                throw new Exception("Квартира не найдена.");
            }

            apartment.IsDeleted = true;
            apartment.DeletionDateUtc = DateTime.UtcNow;
            await this.Context.SaveChangesAsync();
        }
    }
}