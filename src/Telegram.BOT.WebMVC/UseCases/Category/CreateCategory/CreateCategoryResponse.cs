namespace Telegram.BOT.WebMVC.UseCases.Category.CreateCategory
{
    public class CreateCategoryResponse
    {
        public required CategoryResponse Category { get; init; }
    }
    public record CategoryResponse(Guid Id, string Name);
}
