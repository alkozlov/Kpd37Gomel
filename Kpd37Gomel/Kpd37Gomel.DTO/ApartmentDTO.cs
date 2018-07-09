using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    //[DataContract]
    public class ApartmentDTO
    {
        //[DataMember]
        [Key]
        public Guid Id { get; set; }

        //[DataMember]
        public int ApartmentNumber { get; set; }

        //[DataMember]
        public int? FloorNumber { get; set; }

        //[DataMember]
        public double? TotalAreaSnb { get; set; }

        //[DataMember]
        public double TotalArea { get; set; }

        //[DataMember]
        public double? LivingSpace { get; set; }

        //[DataMember]
        public double VoteRate { get; set; }

        //[DataMember]
        public int? RoomsCount { get; set; }

        public List<ApartmentTenantDTO> ApartmentTenants { get; set; }

        public ApartmentDTO()
        {
            this.ApartmentTenants = new List<ApartmentTenantDTO>();
        }
    }
}