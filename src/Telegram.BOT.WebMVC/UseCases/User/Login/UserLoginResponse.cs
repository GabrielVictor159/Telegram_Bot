namespace Telegram.BOT.WebMVC.UseCases.User.Login {
    public class UserLoginResponse {
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
