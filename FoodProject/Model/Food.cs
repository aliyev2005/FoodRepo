using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Model
{
    public class Food:BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? ImageFileName { get; set; }
        public double Price { get; set; }
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
