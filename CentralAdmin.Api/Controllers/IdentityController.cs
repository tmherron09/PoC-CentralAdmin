using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentralAdmin.Api.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        [HttpGet]
        [Authorize(policy: "ApiScope")]
        public IActionResult Get()
        {
            var user = HttpContext.User;
            return Ok(user.Claims.Select(c => new { c.Type, c.Value }));
        }

    }
}
