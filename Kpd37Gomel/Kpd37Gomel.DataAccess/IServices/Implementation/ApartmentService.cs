using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Apartment>> GetApartmentsAsync()
        {
            var apartments = await this.Context.Apartments
                .AsNoTracking()
                .ToListAsync();

            return apartments;
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
            if (apartment != null)
            {
                this.Context.Apartments.Remove(apartment);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}