using DoePaAdminDataModel.DTO;
using DoePaAdmin.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using DoePaAdmin.ViewModel;
using System.Security.Claims;

namespace DoePaAdminWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly ReceiveMitarbeiterPerformanceViewModel _viewModel;

        public EmployeeController(ILogger<EmployeeController> logger, ReceiveMitarbeiterPerformanceViewModel viewModel)
        {
            _logger = logger;
            _viewModel = viewModel;
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
                    IEnumerable<EmployeeAccountingDTO> result = await _viewModel.GetEmployeeAccountingAsync(email, fromDate, toDate);

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
