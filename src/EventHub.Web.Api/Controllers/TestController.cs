using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventHub.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<string> Get()
        {
            var test = new Dictionary<string, string>
            {
                ["Scheme"] = Request.Scheme,
                ["Host"] = Request.Host.ToString(),
                ["X-Forwarded-Proto"] = Request.Headers["X-Forwarded-Proto"],
                ["X-Forwarded-For"] = Request.Headers["X-Forwarded-For"]
            };
            return Ok(test);
        }
    }
}
