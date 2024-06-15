namespace PlayerWalletService.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public Wallet Wallet { get; set; } = new Wallet();
    }
}
