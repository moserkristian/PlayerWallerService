using PlayerWalletService.Models;

namespace PlayerWalletService.Repositories
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();


        public Player GetPlayer(Guid playerId)
        {
            _players.TryGetValue(playerId, out var player);
            return player;
        }

        public void AddPlayer(Player player)
        {
            _players[player.Id] = player;
        }

        public bool PlayerExists(Guid playerId)
        {
            return _players.ContainsKey(playerId);
        }
    }
}
