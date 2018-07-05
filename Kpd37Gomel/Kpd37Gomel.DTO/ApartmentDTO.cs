using System;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class ApartmentDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string ApartmentNumber { get; set; }

        [DataMember]
        public int? FloorNumber { get; set; }

        [DataMember]
        public double? TotalAreaSnb { get; set; }

        [DataMember]
        public double TotalArea { get; set; }

        [DataMember]
        public double? LivingSpace { get; set; }

        [DataMember]
        public double VoteRate { get; set; }

        [DataMember]
        public int? RoomsCount { get; set; }
    }
}