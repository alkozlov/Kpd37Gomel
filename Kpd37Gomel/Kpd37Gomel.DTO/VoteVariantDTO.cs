using System;
using System.Runtime.Serialization;
using Kpd37Gomel.DTO.Converters;
using Newtonsoft.Json;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class VoteVariantDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [JsonConverter(typeof(NullToDefaultConverter<Guid>))]
        public Guid VoteId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}