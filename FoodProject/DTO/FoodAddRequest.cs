using System.ComponentModel.DataAnnotations;

namespace FoodProject.DTO
{
    public class FoodAddRequest
    {
        [Required]
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Guid StoreId { get; set; }
        public double Price { get; set; }
    }
}
