using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace FoodProject.Model
{
    public class Order:BaseModel
    {
        public string Status { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        public Guid Storeid { get; set; }
        public Store Store { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public bool IsDelivered { get; set; }
        public Food Food { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int FoodQuantity { get; set; }
    }
}
