using AutoMapper;
using BCash.Domain.Services;
using BCash.ReportApi.DTOs;
using BCash.TransactionApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BCash.ReportApi.Controllers
{
    [ApiController]
    [Route("v1/report")]
    public class ReportController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        private readonly IBalanceService _balanceService;

        private readonly IDistributedCache _distributedCache;

        private readonly IMapper _mapper;

        private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(1);

        public ReportController(ITransactionService transactionService, IBalanceService balanceService, IDistributedCache distributedCache, IMapper mapper)
        {
            _transactionService = transactionService;
            _balanceService = balanceService;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        [HttpGet("balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetBalanceWithOffsetPagination([FromQuery] DateTime? initDate, [FromQuery] DateTime? endDate, int pageNumber = 1, int pageSize = 10)
        {
            if (!initDate.HasValue || !endDate.HasValue)
                return BadRequest("Init date and end date are required.");

            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest($"{nameof(pageNumber)} and {nameof(pageSize)} size must be greater than 0.");

            string cacheKey = $"report-balance-{initDate.Value:yyyy-MM-dd}-{endDate.Value.ToString("yyyy-MM-dd")}-{pageNumber}-{pageSize}";

            string cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return Ok(JsonSerializer.Deserialize<PagedResponseOffsetDTO<BalanceDTO>>(cachedData));
            }

            var pagedBalances =
                await _balanceService.GetBalancePaged(initDate.Value, endDate.Value, pageNumber, pageSize);

            var pagedBalancesDTO =
                _mapper.Map<PagedResponseOffsetDTO<BalanceDTO>>(pagedBalances);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(pagedBalancesDTO),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
                );

            return Ok(pagedBalancesDTO);
        }

        [HttpGet("transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetTransactionsWithOffsetPagination([FromQuery] DateTime? initDate, [FromQuery] DateTime? endDate, int pageNumber = 1, int pageSize = 10)
        {
            if (!initDate.HasValue || !endDate.HasValue)
                return BadRequest("Init date and end date are required.");

            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest($"{nameof(pageNumber)} and {nameof(pageSize)} size must be greater than 0.");

            string cacheKey = $"report-transactions-{initDate.Value:yyyy-MM-dd}-{endDate.Value.ToString("yyyy-MM-dd")}-{pageNumber}-{pageSize}";

            string cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return Ok(JsonSerializer.Deserialize<PagedResponseOffsetDTO<TransactionDTO>>(cachedData));
            }

            var pagedTransactions =
                await _transactionService.GetTransactionPaged(initDate.Value, endDate.Value, pageNumber, pageSize);

            var pagedTransactionsDTO =
                _mapper.Map<PagedResponseOffsetDTO<TransactionDTO>>(pagedTransactions);

            await _distributedCache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(pagedTransactionsDTO),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
                );

            return Ok(pagedTransactionsDTO);
        }
    }
}
