using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPay.Api.Data;
using SchoolPay.Api.Dtos;
using SchoolPay.Api.Models;

namespace SchoolPay.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolsController : ControllerBase
{
    private readonly AppDbContext _db;

    public SchoolsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<SchoolDto>>> GetAll()
    {
        var schools = await _db.Schools
            .AsNoTracking()
            .Select(s => new SchoolDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Phone = s.Phone,
                Email = s.Email,
                CreatedAt = s.CreatedAt,
                AccountCount = s.Accounts.Count
            })
            .ToListAsync();

        return Ok(schools);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SchoolDetailsDto>> Get(int id)
    {
        var school = await _db.Schools
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new SchoolDetailsDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Phone = s.Phone,
                Email = s.Email,
                CreatedAt = s.CreatedAt,
                AccountCount = s.Accounts.Count,
                Accounts = s.Accounts.Select(a => new AccountDto
                {
                    Id = a.Id,
                    SchoolId = a.SchoolId,
                    AccountName = a.AccountName,
                    AccountNumber = a.AccountNumber,
                    BankName = a.BankName,
                    Balance = a.Balance,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return school is null ? NotFound() : Ok(school);
    }

    [HttpPost]
    public async Task<ActionResult<SchoolDto>> Create(SchoolRequestDto dto)
    {
        var school = new School
        {
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email
        };

        _db.Schools.Add(school);
        await _db.SaveChangesAsync();

        var result = new SchoolDto
        {
            Id = school.Id,
            Name = school.Name,
            Address = school.Address,
            Phone = school.Phone,
            Email = school.Email,
            CreatedAt = school.CreatedAt,
            AccountCount = 0
        };

        return CreatedAtAction(nameof(Get), new { id = school.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SchoolRequestDto dto)
    {
        var school = await _db.Schools.FindAsync(id);
        if (school is null) return NotFound();

        school.Name = dto.Name;
        school.Address = dto.Address;
        school.Phone = dto.Phone;
        school.Email = dto.Email;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var school = await _db.Schools.FindAsync(id);
        if (school is null) return NotFound();

        _db.Schools.Remove(school);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}