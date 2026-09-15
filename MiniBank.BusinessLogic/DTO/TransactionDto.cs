namespace MiniBank.BusinessLogic.DTO
{
    public class TransactionDto
    {
        public string? FromAccountNumber { get; set; }
        public string? ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "MDL";
        public DateTime CreatedAt { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
    }
}
