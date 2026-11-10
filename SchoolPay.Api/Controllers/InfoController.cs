using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolPay.Api.Controllers;


[ApiController]
[Route("")]
[AllowAnonymous]
public class InfoController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiInfo> Get() => Ok(new ApiInfo("SchoolPay API", "0.1.0"));
}

public record ApiInfo(string Name, string Version);
