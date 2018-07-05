using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kpd37Gomel.DTO
{
    [DataContract]
    public class LoginModelDTO
    {
        [DataMember]
        [Required(ErrorMessage = "Имя - обязятельное поле для ввода.")]
        public string FirstName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Отчество - обязятельное поле для ввода.")]
        public string MiddleName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Фамииля - обязятельное поле для ввода.")]
        public string LastName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Номер квартиры - обязятельное поле для ввода.")]
        public string ApartmentNumber { get; set; }
    }
}