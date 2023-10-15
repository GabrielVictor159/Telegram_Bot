using Telegram.BOT.WebMVC.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Marc.UpdateMarc {
    public class UpdateMarcRequest {
        public Telegram.BOT.Domain.Products.Marc marc {  get; set; }
        public List<CategoryResponse> categories { get; set; }
    }
}
