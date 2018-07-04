using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class LoginModelDTO
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}