using Azure.Core;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController(IWalletService walletService) : ControllerBase
    {
        private readonly IWalletService _walletService = walletService;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWallet(string userId)
        {
            try
            {
                var result = await _walletService.GetWalletAsync(userId);
                if (result.Success && result.Data != default)
                {
                    var wallet = result.Data;
                    return Ok(wallet);
                }
                return Problem($"Error: {result.ErrorMessage}");
            }
            catch (Exception ex)
            {
                return Problem($"Error: {ex.Message}");
            }
        }
        [HttpPost("{userId}")]
        //CreateWallet
        public async Task<IActionResult> CreateWallet(string userId)
        {
            try
            {
                var result = await _walletService.CreateWalletAsync(userId);
                if (result.Success && result.Data != default)
                {
                    var wallet = result.Data;
                    return CreatedAtAction(nameof(CreateWallet), new { userId = wallet.UserId }, wallet);
                }
                return Problem($"Error: {result.ErrorMessage}");

            }
            catch (Exception ex)
            {
                return Problem($"Error: {ex.Message}");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateFunds(PatchBalanceRequest request)
        {
            if (request == null || request.UserId == null || request.Amount <= 0)
            {
                return BadRequest("Invalid request data.");
            }

            switch (request.Type)
            {
                case PatchBalanceType.Deposit:
                    return await DepositFunds(request.UserId, request.Amount);

                case PatchBalanceType.Withdraw:
                    return await WithdrawFunds(request.UserId, request.Amount);
                default:
                    return BadRequest("Invalid balance type.");

            }
        }

        //WithdrawFunds
        private async Task<IActionResult> DepositFunds(string userId, decimal amount)
        {
            try
            {
                var result = await _walletService.DepositFundsAsync(userId, amount);

                if (result.Success)
                {
                    return Ok("Funds deposited successfully.");
                }
                return Problem($"Error: {result.ErrorMessage}");
            }
            catch (Exception ex)
            {
                return Problem($"Error: {ex.Message}");
            }
        }


        //WithdrawFunds
        private async Task<IActionResult> WithdrawFunds(string userId, decimal amount)
        {
            try
            {
                var result = await _walletService.WithdrawFundsAsync(userId, amount);
                if (result.Success)
                {
                    return Ok("Funds withdrawn successfully.");
                }
                return Problem($"Error: {result.ErrorMessage}");

            }
            catch (Exception ex)
            {
                return Problem($"Error: {ex.Message}");
            }
        }
    }
}
