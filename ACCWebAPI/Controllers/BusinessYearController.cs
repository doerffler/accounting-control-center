using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("business_years")]
    public class BusinessYearController : ControllerBase
    {
        private readonly ILogger<BusinessYearController> _logger;
        private readonly IACCService _accService;

        public BusinessYearController(ILogger<BusinessYearController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBusinessYears(int? currentPage = 0, int? pageSize = 0)
        {
            try
            {
                IEnumerable<Geschaeftsjahr> geschaeftsjahre = await _accService.GetGeschaeftsjahreAsync(default, currentPage, pageSize);
                int totalCount = geschaeftsjahre.Count();

                var response = new ApiResponseDTO<Geschaeftsjahr>
                {
                    Items = geschaeftsjahre,
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
        public async Task<IActionResult> PostBusinessYear(Geschaeftsjahr geschaeftsjahr)
        {
            try
            {
                geschaeftsjahr.GeschaeftsjahrID = 0;

                Geschaeftsjahr neueGeschaeftsjahr = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Geschaeftsjahre, geschaeftsjahr, default);

                await _accService.SaveChangesAsync();

                return Ok(neueGeschaeftsjahr);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{BusinessYearID}")]
        public async Task<IActionResult> GetBusinessYear(int BusinessYearID)
        {
            try
            {
                Geschaeftsjahr geschaeftsjahr = (await _accService.GetGeschaeftsjahrAsync(BusinessYearID)).FirstOrDefault();
                return Ok(geschaeftsjahr);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{BusinessYearID}")]
        public async Task<IActionResult> PutBusinessYear(int BusinessYearID, Geschaeftsjahr geschaeftsjahr)
        {
            try
            {
                Geschaeftsjahr geschaeftsjahrUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Geschaeftsjahre, BusinessYearID, geschaeftsjahr);

                await _accService.SaveChangesAsync();

                return Ok(geschaeftsjahrUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{BusinessYearID}")]
        public async Task<IActionResult> DeleteBusinessYear(int BusinessYearID)
        {
            try
            {
                IEnumerable<Geschaeftsjahr> geschaeftsjahr = await _accService.GetGeschaeftsjahrAsync(BusinessYearID);
                _accService.RemoveGeschaeftsjahr(geschaeftsjahr.First());

                await _accService.SaveChangesAsync();

                return Ok(geschaeftsjahr.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}