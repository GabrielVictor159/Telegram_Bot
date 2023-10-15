namespace Telegram.BOT.WebMVC.UseCases.Products.CreateProducts {
    public class CreateProductRequest {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public IFormFile Image { get; init; }
        public required string Tags { get; init; }
        public required DateTime CreateDate { get; init; }
        public double Price { get; set; } = 0;
        public required Guid MarcId { get; init; }
    }
}
