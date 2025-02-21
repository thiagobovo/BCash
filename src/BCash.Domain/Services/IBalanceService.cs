using BCash.Domain.Entities;

namespace BCash.Domain.Services
{
    public interface IBalanceService
    {
        Task<PagedResponseOffset<Balance>> GetBalancePaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize);
        Task<Balance> ProcessBalance(decimal amount, DateTime date, string type);
    }
}