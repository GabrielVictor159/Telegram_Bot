namespace Telegram.BOT.WebMVC.UseCases.Marc.GetMarc
{
    public class GetMarcRequest
    {
        public string Name { get; init; } = "";
        public Guid CategoryId { get; init; }
        public int page { get; init; } = 1;
        public int pageSize { get; init; } = 10;
    }
}
