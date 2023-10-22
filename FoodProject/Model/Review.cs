using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Model
{
    public class Review:BaseModel
    {
        public DateTime AddedDate { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public double Rating { get; set; }
    }
}
