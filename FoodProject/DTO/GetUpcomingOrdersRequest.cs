using FoodProject.Model;

namespace FoodProject.DTO
{
    public class GetUpcomingOrdersRequest
    {
        public List<Order> Orders { get; set; }
        public DateTime ElapsedTime { get; set; }
    }
}
