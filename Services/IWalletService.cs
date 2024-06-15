using PlayerWalletService.Models;

namespace PlayerWalletService.Services
{
    public interface IWalletService
    {
        bool RegisterPlayer(Guid playerId);
        decimal GetBalance(Guid playerId);
        (bool Success, string Message) ProcessTransaction(Guid playerId, Transaction transaction);
        IEnumerable<Transaction> GetTransactions(Guid playerId);
    }
}
