namespace Presentation.Dtos;
public enum PatchBalanceType
{
    Deposit,//0
    Withdraw //1
}
public class PatchBalanceRequest
{
    public PatchBalanceType Type { get; set; }
    public string UserId { get; set; } = null!;
    public decimal Amount { get; set; }
}
