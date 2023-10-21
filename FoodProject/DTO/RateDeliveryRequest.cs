using FoodProject.Model;

namespace FoodProject.DTO
{
    public class RateDeliveryRequest
    {
        public Store Store { get; set; }
        public double Rating { get; set; }
        public double GeneralRating { get; set; }
    }
}
