using ManagementServices.variables.Domain.Models;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.CreateEnv
{
    public class CreateEnvRequest {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
