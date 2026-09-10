namespace MiniBank.BusinessLogic.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "MDL";
        public DateTime CreatedAt { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }

    }
}
