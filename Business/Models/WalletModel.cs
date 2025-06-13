using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class WalletModel
{
    [Required]
    public string Id { get; set; } = null!;
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } = 0.00m; //Is has to be a . so input needs to be normalised
}
