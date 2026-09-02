namespace MiniBank.BusinessLogic.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? AccountNumber { get; set; }
        public string? Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
