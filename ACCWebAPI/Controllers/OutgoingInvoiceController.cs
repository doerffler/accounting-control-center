using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.DTO;

using ACCWebAPI.Services;

namespace ACCWebAPI.Controllers
{
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
        public async Task<IActionResult> GetOutgoingInvoices(int? currentPage = 0, int? pageSize = 0)
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
                Ausgangsrechnung ausgangsrechnung = (await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID)).FirstOrDefault();
                _accService.RemoveAusgangsrechnung(ausgangsrechnung);

                await _accService.SaveChangesAsync();

                return Ok(ausgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{OutgoingInvoiceID}/pdf")]
        public async Task<IActionResult> GeneratePdf(int OutgoingInvoiceID)
        {
            Ausgangsrechnung ausgangsrechnung = (await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID)).First();

            PdfService pdfService = new PdfService();
            string result = await pdfService.GeneratePdfAsync(ausgangsrechnung);

            return Ok(result);
        }
    }
}
