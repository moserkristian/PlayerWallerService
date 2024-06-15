using PlayerWalletService.Models;
using PlayerWalletService.Repositories;

namespace PlayerWalletService.Services
{
    public class WalletService : IWalletService
    {
        private readonly IPlayerRepository _playerRepository;

        public WalletService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool RegisterPlayer(Guid playerId)
        {
            if (_playerRepository.PlayerExists(playerId))
                return false;

            var player = new Player { Id = playerId };
            _playerRepository.AddPlayer(player);
            return true;
        }

        public decimal GetBalance(Guid playerId)
        {
            var player = _playerRepository.GetPlayer(playerId);
            return player?.Wallet.Balance ?? 0m;
        }

        public (bool Success, string Message) ProcessTransaction(Guid playerId, Transaction transaction)
        {
            var player = _playerRepository.GetPlayer(playerId);
            if (player == null)
                return (false, "Player not found");

            var existingTransaction = player.Wallet.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (existingTransaction != null)
            {
                return existingTransaction.Type == transaction.Type && existingTransaction.Amount == transaction.Amount
                    ? (true, "Transaction already processed")
                    : (false, "Transaction conflict detected");
            }

            if (transaction.Type == TransactionType.Stake && player.Wallet.Balance < transaction.Amount)
                return (false, "Insufficient balance");

            switch (transaction.Type)
            {
                case TransactionType.Deposit:
                case TransactionType.Win:
                    player.Wallet.Balance += transaction.Amount;
                    break;
                case TransactionType.Stake:
                    player.Wallet.Balance -= transaction.Amount;
                    break;
            }

            player.Wallet.Transactions.Add(transaction);
            return (true, "Transaction processed successfully");
        }

        public IEnumerable<Transaction> GetTransactions(Guid playerId)
        {
            var player = _playerRepository.GetPlayer(playerId);
            return player?.Wallet.Transactions ?? Enumerable.Empty<Transaction>();
        }
    }
}
