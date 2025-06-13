
using Data.Context;
using Data.Entities;
using Data.Results;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class WalletRepository : IWalletRepository
{
    protected readonly DataContext _context;
    protected readonly DbSet<WalletEntity> _wallets;
    public WalletRepository(DataContext context)
    {
        _context = context;
        _wallets = _context.Set<WalletEntity>();
    }

    public async Task<RepositoryResult<WalletEntity?>> CreateAsync(string userId)
    {
        var wallet = new WalletEntity { UserId = userId, Balance = 0.00m };
        try
        {
            var result = await _wallets.AddAsync(wallet);
            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return new RepositoryResult<WalletEntity?>
                {
                    Success = true,
                    Data = result.Entity,
                };
            }
            else
            {
                return new RepositoryResult<WalletEntity?>
                {
                    Success = false,
                    ErrorMessage = "No changes were saved to the database."
                };
            }
        }
        catch (Exception ex)
        {
            return new RepositoryResult<WalletEntity?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while creating the user profile: {ex.Message}"
            };
        }
    }
    public async Task<RepositoryResult<WalletEntity?>> GetByUserIdAsync(string userId)
    {
        try
        {
            var wallet = await _wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet != null)
            {
                return new RepositoryResult<WalletEntity?>
                {
                    Success = true,
                    Data = wallet
                };
            }
            else
            {
                return new RepositoryResult<WalletEntity?>
                {
                    Success = false,
                    ErrorMessage = "Wallet not found."
                };
            }
        }
        catch (Exception ex)
        {
            return new RepositoryResult<WalletEntity?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the wallet: {ex.Message}"
            };
        }
    }
    public async Task<RepositoryResult> UpdateBalanceAsync(string userId, decimal balance)
    {
        try
        {
           var rowsAffected =  await _wallets
                .Where(w => w.UserId == userId)
                .ExecuteUpdateAsync(w => w
                    .SetProperty(w => w.Balance, balance));
            if (rowsAffected > 0)
            {
                return new RepositoryResult { Success = true };
            }
            else
            {
                return new RepositoryResult
                {
                    Success = false,
                    ErrorMessage = "No changes were saved to the database."
                };
            }
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Success = false,
                ErrorMessage = $"An error occurred while updating the wallet: {ex.Message}"
            };
        }
    }
}
