using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.GetEnv {
    public class EnvVariablesController : Controller {

        private readonly IGetEnvByFilterRequest getEnvByFilterRequest;

        public EnvVariablesController(IGetEnvByFilterRequest getEnvByFilterRequest) {
            this.getEnvByFilterRequest = getEnvByFilterRequest;
        }

        public IActionResult Index() {
            var envRequest = new Application.UseCases.Ambient.EnvVariables.GetEnvByFilter.GetEnvByFilterRequest();
            getEnvByFilterRequest.Execute(envRequest);

            if(!envRequest.IsError && envRequest.output != null) {
                var variables = envRequest.variablesFound.Where(x => x.Key != "AdminPass").ToList();
                return View(new GetEnvByFilterResponse { envVariables =  variables});
            } else {
                return View("Error");
            }
        }
    }
}
