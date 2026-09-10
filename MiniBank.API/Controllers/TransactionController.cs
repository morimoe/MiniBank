using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniBank.BusinessLogic.DTO;
using MiniBank.BusinessLogic.Services;
using System.Security.Claims;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> TransactionHistory([FromQuery] int accountId)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var transactions = await _transactionService.TransactionHistoryAsync(accountId, currentUserId);
            return Ok(transactions);
        }

        [Authorize]
        [HttpPost("transfer")]
        public async Task<ActionResult> TransferAsync([FromBody] TransferRequestDto data)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var transfer = await _transactionService.TransferAsync(currentUserId, data.FromAccountNumber, data.ToAccountNumber, data.Amount, data.Description);
            if (transfer.Success == false) { return BadRequest(transfer); }
            return Ok(transfer);
        }
    }
}
