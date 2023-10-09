using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class Food:BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? ImageFileName { get; set; }
    }
}
