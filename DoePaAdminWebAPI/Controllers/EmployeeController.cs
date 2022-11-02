using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace DoePaAdminWebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("employees")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}/invoiced")]
        public string Get(int id, string from, string to)
        {
            return string.Format("User:{0}\nFrom:{1}\nTo:{2}", id, from, to);
        }
    }
}
