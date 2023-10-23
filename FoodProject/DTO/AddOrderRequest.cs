using FoodProject.Model;

namespace FoodProject.DTO
{
    public class AddOrderRequest
    {
        public Guid FoodId { get; set; }
        public int Quantity { get; set; }

    }
}
