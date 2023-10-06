using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc;

namespace Telegram.BOT.WebMVC.UseCases.Marc.DeleteMarc {
    public class MarcController : Controller {

        private readonly IDeleteMarcRequest deleteMarcRequest;

        public MarcController(IDeleteMarcRequest deleteMarcRequest) {
            this.deleteMarcRequest = deleteMarcRequest;
        }

        public IActionResult Delete(string itemid) {
            var deleteRequest = new Application.UseCases.Marc.DeleteMarc.DeleteMarcRequest() { Id = Guid.Parse(itemid) };
            deleteMarcRequest.Execute(deleteRequest);
            if(!deleteRequest.IsError && deleteRequest.output != null) {
                return LocalRedirect("/Marc");
            } else {
                return View("Error");
            }
        }
    }
}
