namespace Telegram.BOT.WebMVC.UseCases.Products.GetProducts {
    public class GetProductRequest {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public string Image { get; set; }
        public required string Tags { get; init; }
        public required DateTime CreateDate { get; init; }
        public double Price { get; set; } = 0;
        public required Guid MarcId { get; init; }
    }
}
