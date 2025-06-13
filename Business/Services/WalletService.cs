
using Business.Models;
using Business.Results;
using Data.Repositories;

namespace Business.Services;

public class WalletService(IWalletRepository walletRepository) : IWalletService
{
    private readonly IWalletRepository _walletRepository = walletRepository;

    public async Task<ServiceResult<WalletModel>> CreateWalletAsync(string userId)
    {
        try
        {
            var result = await _walletRepository.CreateAsync(userId);
            if (result.Success && result.Data != null)
            {
                return new ServiceResult<WalletModel>
                {
                    Success = true,
                    Data = new WalletModel
                    {
                        Id = result.Data.Id,
                        UserId = result.Data.UserId,
                        Balance = result.Data.Balance
                    }
                };
            }
            return new ServiceResult<WalletModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {result.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<WalletModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {ex.Message}"
            };
        };
    }

    public async Task<ServiceResult<WalletModel>> GetWalletAsync(string userId)
    {
        try
        {
            var result = await _walletRepository.GetByUserIdAsync(userId);
            if (result.Success && result.Data != null)
            {
                return new ServiceResult<WalletModel>
                {
                    Success = true,
                    Data = new WalletModel
                    {
                        Id = result.Data.Id,
                        UserId = result.Data.UserId,
                        Balance = result.Data.Balance
                    }
                };
            }
            return new ServiceResult<WalletModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {result.ErrorMessage}"
            };

        }
        catch (Exception ex)
        {
            return new ServiceResult<WalletModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {ex.Message}"
            };
        };
    }

    public async Task<ServiceResult> UpdateBalanceAsync(string userId, decimal balance)
    {
        try
        {
            var result = await _walletRepository.UpdateBalanceAsync(userId, balance);
            if (result.Success)
            {
                return new ServiceResult
                {
                    Success = true
                };
            }
            return new ServiceResult
            {
                Success = false,
                ErrorMessage = $"An error occurred while updating the wallet balance: {result.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult
            {
                Success = false,
                ErrorMessage = $"An error occurred while updating the wallet balance: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResult> DepositFundsAsync(string userId, decimal depositAmount)
    {
        try
        {
            var walletResult = await _walletRepository.GetByUserIdAsync(userId);
            if (walletResult.Success && walletResult.Data != null)
            {
                var result = await UpdateBalanceAsync(userId, walletResult.Data!.Balance + depositAmount);
                return result;
            }
            return new ServiceResult
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {walletResult.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult
            {
                Success = false,
                ErrorMessage = $"An error occurred while adding funds to the wallet: {ex.Message}"
            };
        }
    }

    public async Task<ServiceResult> WithdrawFundsAsync(string userId, decimal withdrawalAmount)
    {
        try
        {
            var walletResult = await _walletRepository.GetByUserIdAsync(userId);
            if (walletResult.Success && walletResult.Data != null)
            {
                if (withdrawalAmount > walletResult.Data.Balance)
                {
                    return new ServiceResult
                    {
                        Success = false,
                        ErrorMessage = "Insufficient funds in the wallet."
                    };

                }
                var result = await UpdateBalanceAsync(userId, walletResult.Data!.Balance - withdrawalAmount);
                return result;
            }
            return new ServiceResult
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {walletResult.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<WalletModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while adding funds to the wallet: {ex.Message}"
            };
        }
    }
}
