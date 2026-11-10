using System.ComponentModel.DataAnnotations;

namespace SchoolPay.Api.Dtos;


public class AccountRequestDto
{

    public int SchoolId { get; set; }

    [Required, MaxLength(100)]
    public string AccountName { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string AccountNumber { get; set; } = string.Empty;

    [MaxLength(100)]
    public string BankName { get; set; } = string.Empty;

    
    [Range(0, double.MaxValue)]
    public decimal OpeningBalance { get; set; }

    public bool IsActive { get; set; } = true;
}