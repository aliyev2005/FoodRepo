using FoodProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class UserAdress:BaseModel
    {        
        [EnumDataType(typeof(Cities))]
        public Cities City { get; set; } = 0;
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        [MaxLength(250,ErrorMessage = "Cannot exceed 250")]
        [MinLength(5,ErrorMessage = "Cannot be less than 5")]
        public string Street { get; set; }
    }
}
