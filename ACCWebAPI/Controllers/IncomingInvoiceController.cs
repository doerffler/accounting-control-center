using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Kostenrechnung;

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
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Eingangsrechnung> result = await _accService.GetEingangsrechnungenAsync();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Store(Eingangsrechnung eingangsrechnung)
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
        public async Task<IActionResult> Show(int IncomingInvoiceID)
        {
            try
            {
                IEnumerable<Eingangsrechnung> eingangsrechnung = await _accService.GetEingangsrechnungAsync(IncomingInvoiceID);
                return Ok(eingangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{IncomingInvoiceID}")]
        public async Task<IActionResult> Update(int IncomingInvoiceID, Eingangsrechnung eingangsrechnung)
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
        public async Task<IActionResult> Destroy(int IncomingInvoiceID)
        {
            try
            {
                IEnumerable<Eingangsrechnung> eingangsrechnung = await _accService.GetEingangsrechnungAsync(IncomingInvoiceID);
                _accService.RemoveEingangsrechnung(eingangsrechnung.First());

                await _accService.SaveChangesAsync();

                return Ok(eingangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
