using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class BaseModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
