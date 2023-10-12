using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class Blacklist:BaseModel
    {
        [Required]
        public int LoginFails { get; set; } = 0;
        [Required]
        public string IpAddress { get; set; }
    }
}
