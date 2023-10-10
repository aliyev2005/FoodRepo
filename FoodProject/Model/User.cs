using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class User:BaseModel
    {
        [Required]
        [MaxLength(200,ErrorMessage = "Cannot exceed 200")]
        [MinLength(2,ErrorMessage = "Cannot be less than 2")]
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
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Token { get; set; }
        public int LoginFails { get; set; } = 0;
    }
}
