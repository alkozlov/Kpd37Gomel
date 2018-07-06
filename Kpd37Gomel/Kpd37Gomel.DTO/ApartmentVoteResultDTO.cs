using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class ApartmentVoteResultDTO
    {
        [DataMember]
        public int ApartmentNumber { get; set; }

        [DataMember]
        public double LivingSpace { get; set; }

        [DataMember]
        public double VoteRate { get; set; }

        [DataMember]
        public string VoteChoise { get; set; }
    }
}