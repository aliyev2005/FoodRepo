using FoodProject.Model;

namespace FoodProject.DTO
{
    public class GetOrderRequest
    {
        public string Status { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public double Price { get; set; }
        public Store Store { get; set; }
        public int Quantity { get; set; }
        public bool IsDelivered { get; set; }
    }
}
