using BCash.Domain.Entities;

namespace BCash.Domain.Services
{
    public interface ITransactionService
    {
        Task CancelTransaction(Guid id);

        Task<Transaction> GetTransaction(Guid id);

        Task<PagedResponseOffset<Transaction>> GetTransactionPaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize);

        Task<Transaction> ProcessTransaction(Transaction transaction);
    }
}