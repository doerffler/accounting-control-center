using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.DTO;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("outgoing_invoices")]
    public class OutgoingInvoiceController : ControllerBase
    {
        private readonly ILogger<OutgoingInvoiceController> _logger;
        private readonly IACCService _accService;

        public OutgoingInvoiceController(ILogger<OutgoingInvoiceController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOutgoingInvoices(int? currentPage, int? pageSize)
        {
            try
            {
                IEnumerable<Ausgangsrechnung> ausgangsrechnungen = await _accService.GetAusgangsrechnungenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetAusgangsrechnungenCountAsync();

                var response = new ApiResponseDTO<Ausgangsrechnung>
                {
                    Items = ausgangsrechnungen,
                    TotalCount = totalCount
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> PostOutgoingInvoice(Ausgangsrechnung ausgangsrechnung)
        {
            try
            {
                ausgangsrechnung.AusgangsrechnungID = 0;

                Ausgangsrechnung neueAusgangsrechnung = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Ausgangsrechnungen, ausgangsrechnung, default);

                await _accService.SaveChangesAsync();

                return Ok(neueAusgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{OutgoingInvoiceID}")]
        public async Task<IActionResult> GetOutgoingInvoice(int OutgoingInvoiceID)
        {
            try
            {
                Ausgangsrechnung ausgangsrechnung = (await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID)).FirstOrDefault();
                return Ok(ausgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{OutgoingInvoiceID}")]
        public async Task<IActionResult> PutOutgoingInvoice(int OutgoingInvoiceID, Ausgangsrechnung ausgangsrechnung)
        {
            try
            {
                Ausgangsrechnung ausgangsrechnungUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Ausgangsrechnungen, OutgoingInvoiceID, ausgangsrechnung);

                await _accService.SaveChangesAsync();

                return Ok(ausgangsrechnungUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{OutgoingInvoiceID}")]
        public async Task<IActionResult> DeleteOutgoingInvoice(int OutgoingInvoiceID)
        {
            try
            {
                IEnumerable<Ausgangsrechnung> ausgangsrechnung = await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID);
                _accService.RemoveAusgangsrechnung(ausgangsrechnung.First());

                await _accService.SaveChangesAsync();

                return Ok(ausgangsrechnung.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
