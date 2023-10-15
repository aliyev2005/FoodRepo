using FoodProject.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class UserAdress:BaseModel
    {        
        [Required]
        [MaxLength(100, ErrorMessage = "Cannot exceed 100")]
        [MinLength(1, ErrorMessage = "Cannot be less than 1")]
        public string Name { get; set; }
        [Required]
        [MaxLength(250,ErrorMessage = "Cannot exceed 250")]
        [MinLength(5,ErrorMessage = "Cannot be less than 5")]
        public string Street { get; set; }
        #region Metadata
        [EnumDataType(typeof(Cities))]
        public Cities City { get; set; } = 0;
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public User User { get; set; }
        #endregion
    }
}
