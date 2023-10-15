namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.UpdateEnv {
    public class UpdateEnvRequest {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
