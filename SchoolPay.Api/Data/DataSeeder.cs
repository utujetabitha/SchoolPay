using SchoolPay.Api.Models;

namespace SchoolPay.Api.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        
        if (db.Schools.Any()) return;

        var schools = new List<School>
        {
            new()
            {
                Name = "Green Hills Academy",
                Address = "KG 9 Ave, Kigali",
                Phone = "0788000001",
                Email = "info@greenhills.rw"
            },
            new()
            {
                Name = "Lycee de Kigali",
                Address = "KN 1 Rd, Nyarugenge, Kigali",
                Phone = "0788000002",
                Email = "contact@lyceedekigali.rw"
            },
            new()
            {
                Name = "FAWE Girls School",
                Address = "Gasabo, Kigali",
                Phone = "0788000003",
                Email = "info@fawegirls.rw"
            }
        };

        db.Schools.AddRange(schools);
        await db.SaveChangesAsync();

        var accounts = new List<Account>
        {
            new()
            {
                SchoolId = schools[0].Id,
                AccountName = "Green Hills Fees Account",
                AccountNumber = "0001-1001-01",
                BankName = "Bank of Kigali",
                Balance = 0
            },
            new()
            {
                SchoolId = schools[0].Id,
                AccountName = "Green Hills Mobile Money",
                AccountNumber = "0788000001",
                BankName = "MTN MoMo",
                Balance = 0
            },
            new()
            {
                SchoolId = schools[1].Id,
                AccountName = "Lycee de Kigali Main Account",
                AccountNumber = "0002-2002-02",
                BankName = "I&M Bank",
                Balance = 0
            },
            new()
            {
                SchoolId = schools[2].Id,
                AccountName = "FAWE Girls Fees Account",
                AccountNumber = "0003-3003-03",
                BankName = "Equity Bank",
                Balance = 0
            }
        };

        db.Accounts.AddRange(accounts);
        await db.SaveChangesAsync();
    }
}