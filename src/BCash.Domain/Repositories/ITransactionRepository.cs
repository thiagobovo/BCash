using BCash.Domain.Entities;

namespace BCash.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> Add(Transaction transaction);

        Task Delete(Guid id);

        Task<PagedResponseOffset<Transaction>> GetByDatePaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize);

        Task<Transaction> GetById(Guid id);
    }
}
