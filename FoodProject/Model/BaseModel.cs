using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
