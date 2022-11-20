using DoePaAdminDataModel.API;
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

        [HttpGet("current/invoiced")]
        public async Task<IActionResult> InvoicedByCurrentUser(string from, string to)
        {
            try
            {
                string? email = User.FindFirst(ClaimTypes.Name)?.Value;
                DateTime fromDate = DateTime.Parse(from);
                DateTime toDate = DateTime.Parse(to).AddMonths(1).AddDays(-1);

                IEnumerable<EmployeeInvoicedHours> result = await _viewModel.GetEmployeeInvoicedHours(email, fromDate, toDate);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
