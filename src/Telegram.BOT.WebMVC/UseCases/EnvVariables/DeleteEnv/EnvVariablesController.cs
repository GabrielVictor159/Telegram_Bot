using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.DeleteEnv {
    public class EnvVariablesController : Controller {

        private readonly IRemoveEnvRequest removeEnvRequest;

        public EnvVariablesController(IRemoveEnvRequest removeEnvRequest) {
            this.removeEnvRequest = removeEnvRequest;
        }

        public IActionResult Delete(string itemid) {
            var deleteRequest = new Application.UseCases.Ambient.EnvVariables.RemoveEnv.RemoveEnvRequest() { Key = itemid };
            removeEnvRequest.Execute(deleteRequest);
            if(!deleteRequest.IsError && deleteRequest != null) {
                return LocalRedirect("/EnvVariables");
            } else {
                return View("Error", (deleteRequest.ErrorMessage, "Index"));
            }
        }
    }
}
