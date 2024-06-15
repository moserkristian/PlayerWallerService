using PlayerWalletService.Models;

namespace PlayerWalletService.Repositories
{
    public interface IPlayerRepository
    {
        Player GetPlayer(Guid playerId);
        void AddPlayer(Player player);
        bool PlayerExists(Guid playerId);
    }
}
