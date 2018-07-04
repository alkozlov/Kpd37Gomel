using System;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class ApartmentTenantDTO
    {
        [DataMember]
        public Guid TenantId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public Guid ApartmentId { get; set; }

        [DataMember]
        public bool IsOwner { get; set; }
    }
}