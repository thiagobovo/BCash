using AutoMapper;
using BCash.Domain.DTOs;
using BCash.Domain.Entities;
using BCash.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BCash.TransactionApi.Controllers
{
    [ApiController]
    [Route("v1/transaction")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        private readonly IBalanceService _balanceService;

        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IBalanceService balanceService, IMapper mapper)
        {
            _transactionService = transactionService;
            _balanceService = balanceService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var resTransaction = await _transactionService.GetTransaction(id);
            if (resTransaction == null)
                return NotFound();
            TransactionDTO transaction = _mapper.Map<TransactionDTO>(resTransaction);
            return Ok(new { transaction });
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] TransactionRequestDTO transactionRequestDTO)
        {
            var transaction = new Transaction(transactionRequestDTO.Amount, transactionRequestDTO.Date, transactionRequestDTO.Type, transactionRequestDTO.Description);
            var createdTransaction = await _transactionService.ProcessTransaction(transaction);
            TransactionDTO transactionDTO = _mapper.Map<TransactionDTO>(createdTransaction);

            var updatedBalance = await _balanceService.ProcessBalance(transactionRequestDTO.Amount, transactionRequestDTO.Date, transactionRequestDTO.Type);
            BalanceDTO balanceDTO = _mapper.Map<BalanceDTO>(updatedBalance);

            return Ok(new { transaction = transactionDTO, balance = balanceDTO });
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deletedTransaction = await _transactionService.GetTransaction(id);
            if (deletedTransaction == null)
                return NotFound();
            await _transactionService.CancelTransaction(id);

            TransactionDTO transaction = _mapper.Map<TransactionDTO>(deletedTransaction);

            var updatedBalance = await _balanceService.ProcessBalance(deletedTransaction.Amount * -1, deletedTransaction.Date, deletedTransaction.Type);
            BalanceDTO balance = _mapper.Map<BalanceDTO>(updatedBalance);

            return Ok(new { transaction, balance });
        }
    }
}
