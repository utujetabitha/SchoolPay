using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPay.Api.Data;
using SchoolPay.Api.Dtos;
using SchoolPay.Api.Models;

namespace SchoolPay.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AccountsController(AppDbContext db) => _db = db;

    private static AccountDto ToDto(Account a) => new()
    {
        Id = a.Id,
        SchoolId = a.SchoolId,
        AccountName = a.AccountName,
        AccountNumber = a.AccountNumber,
        BankName = a.BankName,
        Balance = a.Balance,
        IsActive = a.IsActive,
        CreatedAt = a.CreatedAt
    };

    [HttpGet]
    public async Task<ActionResult<List<AccountDto>>> GetAll()
    {
        var accounts = await _db.Accounts.AsNoTracking().ToListAsync();
        return Ok(accounts.Select(ToDto).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> Get(int id)
    {
        var account = await _db.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        return account is null ? NotFound() : Ok(ToDto(account));
    }

    [HttpGet("school/{schoolId}")]
    public async Task<ActionResult<List<AccountDto>>> GetBySchool(int schoolId)
    {
        if (!await _db.Schools.AnyAsync(s => s.Id == schoolId))
            return NotFound("School not found.");

        var accounts = await _db.Accounts
            .AsNoTracking()
            .Where(a => a.SchoolId == schoolId)
            .ToListAsync();

        return Ok(accounts.Select(ToDto).ToList());
    }

    [HttpPost]
    public async Task<ActionResult<AccountDto>> Create(AccountRequestDto dto)
    {
        if (!await _db.Schools.AnyAsync(s => s.Id == dto.SchoolId))
            return BadRequest("School does not exist.");

        if (await _db.Accounts.AnyAsync(a => a.AccountNumber == dto.AccountNumber))
            return Conflict("An account with this number already exists.");

        var account = new Account
        {
            SchoolId = dto.SchoolId,
            AccountName = dto.AccountName,
            AccountNumber = dto.AccountNumber,
            BankName = dto.BankName,
            Balance = dto.OpeningBalance,
            IsActive = dto.IsActive
        };

        _db.Accounts.Add(account);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = account.Id }, ToDto(account));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AccountRequestDto dto)
    {
        var account = await _db.Accounts.FindAsync(id);
        if (account is null) return NotFound();

        if (await _db.Accounts.AnyAsync(a => a.AccountNumber == dto.AccountNumber && a.Id != id))
            return Conflict("An account with this number already exists.");

        account.AccountName = dto.AccountName;
        account.AccountNumber = dto.AccountNumber;
        account.BankName = dto.BankName;
        account.IsActive = dto.IsActive;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _db.Accounts.FindAsync(id);
        if (account is null) return NotFound();

        _db.Accounts.Remove(account);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}