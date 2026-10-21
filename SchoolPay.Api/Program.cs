using System.Text.Json.Serialization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON options. Enums are sent and received as strings ("Unpaid", not 0).
builder.Services
    .AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// OpenAPI document generation (served at /openapi/v1.json).
builder.Services.AddOpenApi();

// P3 (Day 3, "Talking to other APIs"): register the typed HttpClients for the Provider Sandbox here.
// See Integrations/IMobileMoneyClient.cs and Integrations/ISmsClient.cs. Settings: "Providers" in appsettings.json.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Interactive API explorer at /scalar
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();

// Makes the auto-generated Program class visible to the test project (WebApplicationFactory<Program>).
public partial class Program { }
