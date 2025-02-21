using BCash.Domain.Entities;

namespace BCash.Domain.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> Add(Balance balance);
        Task<Balance> GetByDate(DateTime date);
        Task<PagedResponseOffset<Balance>> GetByDatePaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize);
        Task<Balance> Update(Balance balance);
    }
}
