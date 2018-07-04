using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class VoteResultTinyDTO
    {
        [DataMember]
        public Dictionary<Guid, double> Voices { get; set; }

        public VoteResultTinyDTO()
        {
            this.Voices = new Dictionary<Guid, double>();
        }
    }
}