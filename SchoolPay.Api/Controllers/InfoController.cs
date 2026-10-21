using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolPay.Api.Controllers;

// GET / answers with the API name and version.
// The acceptance tests call it first to check that your API is running. Keep it, and keep it anonymous
// ([AllowAnonymous] still applies when you add authentication in P3).
[ApiController]
[Route("")]
[AllowAnonymous]
public class InfoController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiInfo> Get() => Ok(new ApiInfo("SchoolPay API", "0.1.0"));
}

public record ApiInfo(string Name, string Version);
