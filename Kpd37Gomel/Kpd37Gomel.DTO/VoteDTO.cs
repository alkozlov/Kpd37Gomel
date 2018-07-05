using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kpd37Gomel.DTO.Converters;
using Newtonsoft.Json;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class VoteDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public TenantDTO Author { get; set; }

        [DataMember]
        public DateTime CreateDateUtc { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Тема опроса не указана.")]
        [MaxLength(150, ErrorMessage = "Максимальная длина темы 150 символов.")]
        public string Title { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Не указано подробное описание опроса.")]
        [MaxLength(500, ErrorMessage = "Максимальная длина подробного описания 150 символов.")]
        public string Description { get; set; }

        [DataMember]
        public bool UseVoteRate { get; set; }

        [DataMember]
        public List<VoteVariantDTO> Variants { get; set; }

        public VoteDTO()
        {
            this.Variants = new List<VoteVariantDTO>();
        }
    }
}