namespace MiniBank.BusinessLogic.DTO
{
    public class TransferResultDto
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public static TransferResultDto Fail(string message)
        {
            return new TransferResultDto { Success = false, ErrorMessage = message };
        }

        public static TransferResultDto Pass()
        {
            return new TransferResultDto { Success = true };
        }
    }
}
