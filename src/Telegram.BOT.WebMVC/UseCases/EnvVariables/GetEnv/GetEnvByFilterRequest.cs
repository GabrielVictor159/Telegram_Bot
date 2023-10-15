using ManagementServices.variables.Domain.Models;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.GetEnv {
    public class GetEnvByFilterRequest {
        public string Key { get; init; } = "";
        public string Value { get; init; } = "";
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public List<EnvVariable> variablesFound { get; set; } = new();
    }
}
