namespace BCash.Domain.DTOs
{
    public class BalanceDTO
    {
        public decimal TotalCredit { get; set; }

        public decimal TotalDebit { get; set; }

        public DateTime Date { get; set; }
    }
}
