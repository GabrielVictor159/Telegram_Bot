using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Marc.DeleteMarc;

namespace Telegram.BOT.WebMVC.UseCases.Marc.DeleteMarc {
    public class MarcController : Controller {

        private readonly IDeleteMarcRequest deleteMarcRequest;

        public MarcController(IDeleteMarcRequest deleteMarcRequest) {
            this.deleteMarcRequest = deleteMarcRequest;
        }

        public IActionResult Delete(DeleteMarcRequest request) {
            var deleteRequest = new Application.UseCases.Marc.DeleteMarc.DeleteMarcRequest() { Id = request.Id };
            deleteMarcRequest.Execute(deleteRequest);
            if(!deleteRequest.IsError && deleteRequest.output != null) {
                return LocalRedirect("/Category");
            } else {
                return View("Error");
            }
        }
    }
}
