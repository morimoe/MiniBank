namespace MiniBank.BusinessLogic.DTO
{
    public class LoginResultDto
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }

        public static LoginResultDto Fail(string message)
        {
            return new LoginResultDto { Success = false, ErrorMessage = message };
        }

        public static LoginResultDto Pass(string token)
        {
            return new LoginResultDto { Success = true, Token = token };
        }
    }
}
