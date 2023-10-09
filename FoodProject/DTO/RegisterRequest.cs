using System.ComponentModel.DataAnnotations;

namespace FoodProject.DTO
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Cannot exceed 200")]
        [MinLength(2, ErrorMessage = "Cannot be less than 2")]
        public string Fullname { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100, ErrorMessage = "Cannot be more than 100")]
        [MinLength(3, ErrorMessage = "Cannot be less than 3")]
        public string Email { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Cannot be more than 250")]
        [MinLength(6, ErrorMessage = "Cannot be less than 6")]
        public string Password { get; set; }
    }
}
