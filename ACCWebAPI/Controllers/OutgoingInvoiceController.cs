﻿using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Kostenrechnung;

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
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Ausgangsrechnung> result = await _accService.GetAusgangsrechnungenAsync();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Store(Ausgangsrechnung ausgangsrechnung)
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
        public async Task<IActionResult> Show(int OutgoingInvoiceID)
        {
            try
            {
                IEnumerable<Ausgangsrechnung> ausgangsrechnung = await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID);
                return Ok(ausgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{OutgoingInvoiceID}")]
        public async Task<IActionResult> Update(int OutgoingInvoiceID, Ausgangsrechnung ausgangsrechnung)
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
        public async Task<IActionResult> Destroy(int OutgoingInvoiceID)
        {
            try
            {
                IEnumerable<Ausgangsrechnung> ausgangsrechnung = await _accService.GetAusgangsrechnungAsync(OutgoingInvoiceID);
                _accService.RemoveAusgangsrechnung(ausgangsrechnung.First());

                await _accService.SaveChangesAsync();

                return Ok(ausgangsrechnung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
