using Telegram.BOT.WebMVC.UseCases.Category.GetCategory;
using Telegram.BOT.WebMVC.UseCases.Marc.GetMarc;

namespace Telegram.BOT.WebMVC.UseCases.Marc.CreateMarc
{
    public class CreateMarcRequest
    {
        public required string Name { get; init; }
        public required Guid CategoryId { get; init; }
    }
}
