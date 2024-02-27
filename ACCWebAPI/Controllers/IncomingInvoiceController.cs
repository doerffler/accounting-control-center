using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.DTO;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("incoming_invoices")]
    public class IncomingInvoiceController : ControllerBase
    {
        private readonly ILogger<IncomingInvoiceController> _logger;
        private readonly IACCService _accService;

        public IncomingInvoiceController(ILogger<IncomingInvoiceController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetIncomingInvoices(int? currentPage, int? pageSize)
        {
            try
            {
                IEnumerable<Eingangsrechnung> eingangsrechnungen = await _accService.GetEingangsrechnungenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetEingangsrechnungenCountAsync();

                var response = new ApiResponseDTO<Eingangsrechnung>
                {
                    Items = eingangsrechnungen,
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
        public async Task<IActionResult> PostIncomingInvoice(Eingangsrechnung eingangsrechnung)
        {
            try
            {
                eingangsrechnung.EingangsrechnungID = 0;

                Eingangsrechnung neueEingangsrechnung = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Eingangsrechnungen, eingangsrechnung, default);

                await _accService.SaveChangesAsync();

                return Ok(neueEingangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{IncomingInvoiceID}")]
        public async Task<IActionResult> GetIncomingInvoice(int IncomingInvoiceID)
        {
            try
            {
                Eingangsrechnung eingangsrechnung = (await _accService.GetEingangsrechnungAsync(IncomingInvoiceID)).FirstOrDefault();
                return Ok(eingangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{IncomingInvoiceID}")]
        public async Task<IActionResult> PutIncomingInvoice(int IncomingInvoiceID, Eingangsrechnung eingangsrechnung)
        {
            try
            {
                Eingangsrechnung eingangsrechnungUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Eingangsrechnungen, IncomingInvoiceID, eingangsrechnung);

                await _accService.SaveChangesAsync();

                return Ok(eingangsrechnungUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{IncomingInvoiceID}")]
        public async Task<IActionResult> DeleteIncomingInvoice(int IncomingInvoiceID)
        {
            try
            {
                IEnumerable<Eingangsrechnung> eingangsrechnung = await _accService.GetEingangsrechnungAsync(IncomingInvoiceID);
                _accService.RemoveEingangsrechnung(eingangsrechnung.First());

                await _accService.SaveChangesAsync();

                return Ok(eingangsrechnung.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
