// P3, Day 3 ("Talking to other APIs"). Leave this file commented out until then: P1 and P2 do not need it.
//
// IMobileMoneyClient is how SchoolPay talks to a mobile-money provider. Today the provider is the
// Provider Sandbox (http://localhost:5099). Because the rest of the API only knows this interface,
// switching to a real provider later means a new class and new settings, not a rewrite.
//
// How to start in P3:
//   1. Uncomment the code below.
//   2. Write a class MomoClient : IMobileMoneyClient that receives an HttpClient in its constructor.
//   3. Register it as a typed client in Program.cs, with BaseAddress and the X-Api-Key header
//      from the "Providers" section of appsettings.json:
//        builder.Services.AddHttpClient<IMobileMoneyClient, MomoClient>(...);
//   4. Map provider failures in your service: 422 PAYER_REJECTED -> payment Failed + 502,
//      provider unreachable (HttpRequestException, timeout) -> payment Failed + 503.
//
// Contract: 04-pair-project/api-contract.md. Worked example: 06-code/integration-demo/.

// namespace SchoolPay.Api.Integrations;
//
// public interface IMobileMoneyClient
// {
//     // POST {SandboxBaseUrl}/momo/v1/requesttopay  -> 202 { providerReference, status: "PENDING" }
//     Task<RequestToPayResult> RequestToPayAsync(RequestToPayRequest request, CancellationToken ct = default);
//
//     // GET {SandboxBaseUrl}/momo/v1/requesttopay/{providerReference}  -> current status (optional, for polling)
//     Task<RequestToPayResult> GetStatusAsync(string providerReference, CancellationToken ct = default);
// }
//
// // Amounts are whole Rwandan francs: int, never decimal or double.
// public record RequestToPayRequest(
//     int Amount,
//     string Currency,           // always "RWF"
//     string PayerPhone,
//     string ExternalReference,  // your payment id, as a string
//     string CallbackUrl);       // {CallbackBaseUrl}/api/webhooks/momo
//
// public record RequestToPayResult(string ProviderReference, string Status); // PENDING | SUCCESSFUL | FAILED
