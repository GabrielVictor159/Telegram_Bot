using Telegram.BOT.WebMVC.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Marc.GetMarc
{
    public class GetMarcResponse
    {
        public List<CategoryResponse> Categories { get; init; } = new();
        public List<MarcResponse> Marcs { get; init; } = new();
        
    }
    public record MarcResponse(Guid Id, string Name, Domain.Products.Category Category, Guid CategoryId);
    
}
