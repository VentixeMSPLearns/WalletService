using Data.Entities;
using Data.Results;

namespace Data.Repositories
{
    public interface IWalletRepository
    {
        Task<RepositoryResult<WalletEntity?>> CreateAsync(string userId);
        Task<RepositoryResult<WalletEntity?>> GetByUserIdAsync(string userId);
        Task<RepositoryResult> UpdateBalanceAsync(string id, decimal balance);
    }
}