namespace Telegram.BOT.WebMVC.UseCases.Category.GetCategory
{
    public class GetCategoryResponse
    {
        public required List<CategoryResponse> CategoryResponse { get; init; }
    }
    public record CategoryResponse(Guid id, string Name);
   
}
