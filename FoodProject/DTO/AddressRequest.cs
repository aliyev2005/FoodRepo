using FoodProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.DTO
{
    public class AddressRequest
    {
        [EnumDataType(typeof(Cities))]
        public Cities City { get; set; } = Cities.BAKU;
        [Required]
        [MaxLength(100, ErrorMessage = "Cannot exceed 100")]
        [MinLength(1, ErrorMessage = "Cannot be less than 1")]
        public string Name { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Cannot exceed 250")]
        [MinLength(5, ErrorMessage = "Cannot be less than 5")]
        public string Street { get; set; }
    }
}
