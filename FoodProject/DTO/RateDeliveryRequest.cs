using FoodProject.Model;

namespace FoodProject.DTO
{
    public class RateDeliveryRequest
    {
        public Store Store { get; set; }
        public Order Order { get; set; }
    }
}
