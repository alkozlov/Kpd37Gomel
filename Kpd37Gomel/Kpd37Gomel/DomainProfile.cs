using AutoMapper;
using Kpd37Gomel.DataAccess.Models;
using Kpd37Gomel.DTO;

namespace Kpd37Gomel
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            this.CreateMap<Apartment, ApartmentDTO>();
            this.CreateMap<Tenant, TenantDTO>();
            this.CreateMap<VoteVariant, VoteVariantDTO>();
            this.CreateMap<VoteChoice, VoteChoiceDTO>();
            this.CreateMap<Vote, VoteDTO>();

            this.CreateMap<ApartmentDTO, Apartment>();
            this.CreateMap<TenantDTO, Tenant>();
            this.CreateMap<VoteVariantDTO, VoteVariant>();
            this.CreateMap<VoteChoiceDTO, VoteChoice>();
            this.CreateMap<VoteDTO, Vote>();
        }
    }
}