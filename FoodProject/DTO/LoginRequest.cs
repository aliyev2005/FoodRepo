using System.ComponentModel.DataAnnotations;

namespace FoodProject.DTO
{
    public class LoginRequest
    {
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
