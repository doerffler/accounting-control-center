using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("currencies")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly IACCService _accService;

        public CurrencyController(ILogger<CurrencyController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCurrencies(int? currentPage, int? pageSize)
        {
            try
            {
                IEnumerable<Waehrung> waehrungen = await _accService.GetWaehrungenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetWaehrungenCountAsync();

                var response = new ApiResponseDTO<Waehrung>
                {
                    Items = waehrungen,
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
        public async Task<IActionResult> PostCurrency(Waehrung waehrung)
        {
            try
            {
                waehrung.WaehrungID = 0;

                Waehrung neueWaehrung = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Waehrungen, waehrung, default);

                await _accService.SaveChangesAsync();

                return Ok(neueWaehrung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{CurrencyID}")]
        public async Task<IActionResult> GetCurrency(int CurrencyID)
        {
            try
            {
                Waehrung waehrung = (await _accService.GetWaehrungAsync(CurrencyID)).FirstOrDefault();
                return Ok(waehrung);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{CurrencyID}")]
        public async Task<IActionResult> PutCurrency(int CurrencyID, Waehrung waehrung)
        {
            try
            {
                Waehrung waehrungUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Waehrungen, CurrencyID, waehrung);

                await _accService.SaveChangesAsync();

                return Ok(waehrungUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{CurrencyID}")]
        public async Task<IActionResult> DeleteCurrency(int CurrencyID)
        {
            try
            {
                IEnumerable<Waehrung> waehrung = await _accService.GetWaehrungAsync(CurrencyID);
                _accService.RemoveWaehrung(waehrung.First());

                await _accService.SaveChangesAsync();

                return Ok(waehrung.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
