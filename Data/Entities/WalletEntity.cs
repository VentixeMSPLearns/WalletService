using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class WalletEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]//FK
    public string UserId { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } = 0.00m; //Is has to be a . so input needs to be normalised
}
