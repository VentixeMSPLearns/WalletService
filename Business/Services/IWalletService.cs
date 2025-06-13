using Business.Models;
using Business.Results;

namespace Business.Services
{
    public interface IWalletService
    {
        Task<ServiceResult<WalletModel>> CreateWalletAsync(string userId);
        Task<ServiceResult> DepositFundsAsync(string userId, decimal depositAmount);
        Task<ServiceResult<WalletModel>> GetWalletAsync(string userId);
        Task<ServiceResult> UpdateBalanceAsync(string userId, decimal balance);
        Task<ServiceResult> WithdrawFundsAsync(string userId, decimal withdrawalAmount);
    }
}