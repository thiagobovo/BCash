using BCash.Domain.Entities;
using BCash.Domain.Repositories;
using BCash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BCash.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SqlDbContext _context;

        public TransactionRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
           await _context.Transactions.AddAsync(transaction);
           await _context.SaveChangesAsync();
           return transaction;
        }

        public async Task Delete(Guid id)
        {
            await _context.Transactions.Where(x => id.Equals(x.Id)).ExecuteDeleteAsync();
        }

        public async Task<PagedResponseOffset<Transaction>> GetByDatePaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Transactions.AsNoTracking()
                .Where(f => f.Date >= initDate && f.Date <= endDate)
                .CountAsync();

            var transactions = await _context.Transactions.AsNoTracking()
                .Where(f => f.Date >= initDate && f.Date <= endDate)
                .OrderByDescending(x => x.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponseOffset<Transaction>(transactions, pageNumber, pageSize, totalRecords);
        }

        public async Task<Transaction> GetById(Guid id) => await _context.Transactions.FindAsync(id);
    }
}
