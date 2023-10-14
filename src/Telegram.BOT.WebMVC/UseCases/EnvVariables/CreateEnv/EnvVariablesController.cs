using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.CreateEnv {
    public class EnvVariablesController : Controller {

        private readonly ICreateEnvRequest createEnvRequest;

        public EnvVariablesController(ICreateEnvRequest createEnvRequest) {
            this.createEnvRequest = createEnvRequest;
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAction(CreateEnvRequest request) {
            var createRequest = new Application.UseCases.Ambient.EnvVariables.CreateEnv.CreateEnvRequest() {
                EnvVariable = new ManagementServices.variables.Domain.Models.EnvVariable() { Key = request.Key, Value = request.Value }
            };
            createEnvRequest.Execute(createRequest);
            if(!createRequest.IsError && createRequest.output != null) {
                return LocalRedirect("/EnvVariables");
            } else {
                return View("Error", (createRequest.ErrorMessage, "Index"));
            }
        }
    }
}
