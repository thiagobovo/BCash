using BCash.Domain.Entities;
using BCash.Domain.Repositories;
using BCash.Domain.Services;

namespace BCash.Service.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceService(IBalanceRepository balanceService)
        {
            _balanceRepository = balanceService;
        }

        public async Task<PagedResponseOffset<Balance>> GetBalancePaged(DateTime initDate, DateTime endDate, int pageNumber, int pageSize)
        {
            return await _balanceRepository.GetByDatePaged(initDate, endDate, pageNumber, pageSize);
        }

        public async Task<Balance> ProcessBalance(decimal amount, DateTime date, string type)
        {
            var balance = await _balanceRepository.GetByDate(date);

            decimal totalCredit = type.Equals("C") ? amount : 0;
            decimal totalDebit = type.Equals("D") ? amount : 0;

            if (balance == null)
            {
                balance = new Balance(totalCredit, totalDebit, date);
                return await _balanceRepository.Add(balance);
            }
            else
            {
                balance.TotalCredit = balance.TotalCredit + totalCredit;
                balance.TotalDebit = balance.TotalDebit + totalDebit;
                return await _balanceRepository.Update(balance);
            }
        }
    }
}
