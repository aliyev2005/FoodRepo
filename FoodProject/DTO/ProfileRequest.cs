using System.ComponentModel.DataAnnotations;

namespace FoodProject.DTO
{
    public class ProfileRequest
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
        [Phone]
        public string? PhoneNumber { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
