using Microsoft.AspNetCore.Mvc;
using PlayerWalletService.Models;
using PlayerWalletService.Services;

namespace PlayerWalletService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("register")]
        public IActionResult RegisterPlayer([FromQuery] Guid playerId)
        {
            if (_walletService.RegisterPlayer(playerId))
                return Ok("Player registered successfully");
            return Conflict("Player already registered");
        }

        [HttpGet("balance/{playerId}")]
        public IActionResult GetBalance(Guid playerId)
        {
            var balance = _walletService.GetBalance(playerId);
            return Ok(balance);
        }

        [HttpPost("transaction/{playerId}")]
        public IActionResult ProcessTransaction(Guid playerId, [FromBody] Transaction transaction)
        {
            var (success, message) = _walletService.ProcessTransaction(playerId, transaction);
            if (success)
                return Ok(message);
            return BadRequest(message);
        }

        [HttpGet("transactions/{playerId}")]
        public IActionResult GetTransactions(Guid playerId)
        {
            var transactions = _walletService.GetTransactions(playerId);
            return Ok(transactions);
        }
    }
}
