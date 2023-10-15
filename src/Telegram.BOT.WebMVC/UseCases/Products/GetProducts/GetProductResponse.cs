using Telegram.BOT.WebMVC.UseCases.Marc.GetMarc;

namespace Telegram.BOT.WebMVC.UseCases.Products.GetProducts {
    public class GetProductResponse {
        public List<ProductResponse> Products { get; init; } = new();
        public List<MarcResponse> Marcs { get; init; } = new();
    }
    public record ProductResponse(
        Guid id, string name, string description, string image, string tags, DateTime CreateDate, double price, Domain.Products.Marc Marc, Guid MarcId);
}
