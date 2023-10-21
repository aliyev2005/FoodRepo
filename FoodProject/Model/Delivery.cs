using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Model
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        public double Rating { get; set; }
    }
}
