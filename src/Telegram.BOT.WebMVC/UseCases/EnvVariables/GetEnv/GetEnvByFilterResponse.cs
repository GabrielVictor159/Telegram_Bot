using ManagementServices.variables.Domain.Models;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.GetEnv {
    public class GetEnvByFilterResponse {
        public required List<EnvVariable> envVariables { get; set; } = new();
    }
}
