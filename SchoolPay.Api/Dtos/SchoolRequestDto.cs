using System.ComponentModel.DataAnnotations;

namespace SchoolPay.Api.Dtos;


public class SchoolRequestDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;
}