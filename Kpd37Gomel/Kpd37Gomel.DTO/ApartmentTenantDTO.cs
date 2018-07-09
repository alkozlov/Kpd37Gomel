using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    //[DataContract]
    public class ApartmentTenantDTO
    {
        //[DataMember]
        [Key]
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }

        //[DataMember]
        [Key]
        [ForeignKey("Apartment")]
        public Guid ApartmentId { get; set; }

        //[DataMember]
        public bool IsOwner { get; set; }

        public ApartmentDTO Apartment { get; set; }
        public TenantDTO Tenant { get; set; }
    }
}