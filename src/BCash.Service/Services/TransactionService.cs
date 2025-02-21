using BCash.Domain.Entities;
using BCash.Domain.Repositories;
using BCash.Domain.Services;

namespace BCash.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task CancelTransaction(Guid id)
        {
            await _transactionRepository.Delete(id);
        }

        public async Task<Transaction> GetTransaction(Guid id)
        {
            return await _transactionRepository.GetById(id);
        }

        public async Task<PagedResponseOffset<Transaction>> GetTransactionPaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize)
        {
            return await _transactionRepository.GetByDatePaged(initDate, endDate, pageNumber, pageSize);
        }

        public async Task<Transaction> ProcessTransaction(decimal amount, DateTime date, string type, string? description)
        {
            var transaction = new Transaction(amount, date, type, description);

            return await _transactionRepository.Add(transaction);
        }
    }
}
