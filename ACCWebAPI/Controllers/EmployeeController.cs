using ACCDataModel.DTO;
using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IACCService _accService;

        public EmployeeController(ILogger<EmployeeController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("current/accounted")]
        public async Task<IActionResult> GetAccountingForCurrentUser(string from, string to)
        {
            try
            {
                
                bool datesParsed = false;

                string? email = User.FindFirst(ClaimTypes.Name)?.Value;

                DateTime fromDate = DateTime.MinValue;
                DateTime toDate = DateTime.MaxValue;

                datesParsed = DateTime.TryParse(from, out fromDate);
                datesParsed = datesParsed && DateTime.TryParse(to, out toDate);

                if (!string.IsNullOrEmpty(email) && datesParsed)
                {
                    IEnumerable<EmployeeAccountingDTO> result = await _accService.GetEmployeeAccountingAsync(email, fromDate, toDate);

                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
                                
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
