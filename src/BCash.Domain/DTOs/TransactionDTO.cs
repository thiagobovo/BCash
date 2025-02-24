namespace BCash.Domain.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string? Type { get; set; }

        public string? Description { get; set; }
    }
}
