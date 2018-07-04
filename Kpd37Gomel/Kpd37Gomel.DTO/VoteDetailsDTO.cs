using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class VoteDetailsDTO
    {
        [DataMember]
        public bool IsPassed { get; set; }

        [DataMember]
        public VoteDTO Vote { get; set; }

        [DataMember]
        public VoteResultTinyDTO Result { get; set; }
    }
}