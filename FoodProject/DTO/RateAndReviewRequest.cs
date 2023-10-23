namespace FoodProject.DTO
{
    public class RateAndReviewRequest
    {
        public string Title { get; set; }
        public double Rating { get; set; }
        public Guid StoreId { get; set; }
    }
}
