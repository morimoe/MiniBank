namespace MiniBank.BusinessLogic.DTO
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? Currency { get; set; }
        public decimal Balance { get; set; }
    }
}