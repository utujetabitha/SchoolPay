// P3, Day 3 ("Talking to other APIs"). Leave this file commented out until then: P1 and P2 do not need it.
//
// ISmsClient sends SMS receipts through the SMS provider. Today the provider is the Provider Sandbox
// (http://localhost:5099); the message appears in the inbox of that number on the sandbox phone screen.
//
// How to start in P3:
//   1. Uncomment the code below.
//   2. Write a class SmsClient : ISmsClient that receives an HttpClient in its constructor.
//   3. Register it as a typed client in Program.cs, with BaseAddress and the X-Api-Key header
//      from the "Providers" section of appsettings.json:
//        builder.Services.AddHttpClient<ISmsClient, SmsClient>(...);
//   4. Send the receipt after a payment succeeds or fails (texts in api-contract.md). Send it once:
//      a duplicate webhook must not send a second SMS.
//
// Contract: 04-pair-project/api-contract.md. Worked example: 06-code/integration-demo/.

// namespace SchoolPay.Api.Integrations;
//
// public interface ISmsClient
// {
//     // POST {SandboxBaseUrl}/sms/v1/messages { to, message }  -> 201 { messageId, status: "QUEUED" }
//     Task<SmsResult> SendAsync(string to, string message, CancellationToken ct = default);
// }
//
// public record SmsResult(string MessageId, string Status);
