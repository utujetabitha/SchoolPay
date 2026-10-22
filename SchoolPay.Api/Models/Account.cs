using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolPay.Api.Models;

public class Account
{
    public int Id { get; set; }

    [Required]
    public int SchoolId { get; set; }

    [JsonIgnore]
    public School? School { get; set; }

    [Required, MaxLength(100)]
    public string AccountName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string AccountNumber { get; set; } = string.Empty;

    [MaxLength(100)]
    public string BankName { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}