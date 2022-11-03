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
        private ReceiveMitarbeiterPerformanceViewModel _viewModel;

        public EmployeeController(ILogger<EmployeeController> logger, ReceiveMitarbeiterPerformanceViewModel viewModel)
        {
            _logger = logger;
            _viewModel = viewModel;
        }

        [HttpGet("{id:int}/invoiced")]
        public IEnumerable<EmployeeInvoicedHours> InvoicedByUserId(int id, string from, string to)
        {

            return new List<EmployeeInvoicedHours>();
        }

        [HttpGet("current/invoiced")]
        public IEnumerable<EmployeeInvoicedHours> InvoicedByCurrentUser(string from, string to)
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;

            return new List<EmployeeInvoicedHours>();
        }
    }
}
