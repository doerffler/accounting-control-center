using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("performance_records")]
    public class PerformanceRecordController : ControllerBase
    {
        private readonly ILogger<PerformanceRecordController> _logger;
        private readonly IACCService _accService;

        public PerformanceRecordController(ILogger<PerformanceRecordController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPerformanceRecords(int? currentPage, int? pageSize)
        {
            try
            {
                IEnumerable<Leistungsnachweis> leistungsnachweise = await _accService.GetLeistungsnachweiseAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetLeistungsnachweiseCountAsync();

                var response = new ApiResponseDTO<Leistungsnachweis>
                {
                    Items = leistungsnachweise,
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
        public async Task<IActionResult> PostPerformanceRecord(Leistungsnachweis leistungsnachweis)
        {
            try
            {
                leistungsnachweis.LeistungsnachweisID = 0;

                Leistungsnachweis neuerLeistungsnachweis = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Leistungsnachweise, leistungsnachweis, default);

                await _accService.SaveChangesAsync();

                return Ok(neuerLeistungsnachweis);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{PerformanceRecordID}")]
        public async Task<IActionResult> GetPerformanceRecord(int PerformanceRecordID)
        {
            try
            {
                Leistungsnachweis leistungsnachweis = (await _accService.GetLeistungsnachweisAsync(PerformanceRecordID)).FirstOrDefault();
                return Ok(leistungsnachweis);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{PerformanceRecordID}")]
        public async Task<IActionResult> PutPerformanceRecord(int PerformanceRecordID, Leistungsnachweis leistungsnachweis)
        {
            try
            {
                Leistungsnachweis leistungsnachweisUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Leistungsnachweise, PerformanceRecordID, leistungsnachweis);

                await _accService.SaveChangesAsync();

                return Ok(leistungsnachweisUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{PerformanceRecordID}")]
        public async Task<IActionResult> DeletePerformanceRecord(int PerformanceRecordID)
        {
            try
            {
                IEnumerable<Leistungsnachweis> leistungsnachweis = await _accService.GetLeistungsnachweisAsync(PerformanceRecordID);
                _accService.RemoveLeistungsnachweis(leistungsnachweis.First());

                await _accService.SaveChangesAsync();

                return Ok(leistungsnachweis.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
