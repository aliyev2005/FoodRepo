namespace FoodProject.Model
{
    public class Store:BaseModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? BackImage { get; set; }
        public string? Logo { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Order> Orders { get; set; }
        public List<Food> Foods { get; set; }
        public bool IsFavorited { get; set; }
    }
}
