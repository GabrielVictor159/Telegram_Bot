using ManagementServices.variables.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.UpdateEnv;
using Telegram.BOT.WebMVC.UseCases.EnvVariables.GetEnv;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.UpdateEnv {
    public class EnvVariablesController : Controller {

        private readonly IGetEnvRequest getEnvRequest;
        private readonly IUpdateEnvRequest updateEnvRequest;

        public EnvVariablesController(IGetEnvRequest getEnvRequest, IUpdateEnvRequest updateEnvRequest) {
            this.getEnvRequest = getEnvRequest;
            this.updateEnvRequest = updateEnvRequest;
        }

        public IActionResult Edit(string itemid) {
            var envRequest = new Application.UseCases.Ambient.EnvVariables.GetEnv.GetEnvRequest() { Key = itemid };
            getEnvRequest.Execute(envRequest);
            if (!envRequest.IsError && envRequest.output != null) {
                return View(envRequest.EnvVariable);
            } else {
                return View("Error", (envRequest.ErrorMessage, "Index"));
            }
        }

        [HttpPost]
        public IActionResult EditAction(EnvVariable variable) {
            var updateRequest = new Application.UseCases.Ambient.EnvVariables.UpdateEnv.UpdateEnvRequest() { variable = variable };
            updateEnvRequest.Execute(updateRequest);
            if (!updateRequest.IsError && updateRequest.output != null) {
                return LocalRedirect("/EnvVariables");
            } else {
                return View("Error", (updateRequest.ErrorMessage, nameof(Edit)));
            }
        }
    }
}
