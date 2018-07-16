using System;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class VoteChoiseTinyDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public double Value { get; set; }
    }
}