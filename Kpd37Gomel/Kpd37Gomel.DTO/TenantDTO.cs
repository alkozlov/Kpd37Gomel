using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class TenantDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Имя не указано.")]
        [MaxLength(500, ErrorMessage = "Максимальная длина имени 100 символов.")]
        public string FirstName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Отчество не указано.")]
        [MaxLength(500, ErrorMessage = "Максимальная длина отчества 100 символов.")]
        public string MiddleName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Фамилия не указана.")]
        [MaxLength(500, ErrorMessage = "Максимальная длина фамилия 100 символов.")]
        public string LastName { get; set; }

        [DataMember]
        public bool IsOwner { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }
    }
}