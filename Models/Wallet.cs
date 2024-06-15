namespace PlayerWalletService.Models
{
    public class Wallet
    {
        public decimal Balance { get; set; } = 0m;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
