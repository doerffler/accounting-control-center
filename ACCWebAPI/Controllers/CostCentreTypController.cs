using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cost_centre_types")]
    public class CostCentreTypeController : ControllerBase
    {
        private readonly ILogger<CostCentreTypeController> _logger;
        private readonly IACCService _accService;

        public CostCentreTypeController(ILogger<CostCentreTypeController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCostCentreTypes(int? currentPage = 0, int? pageSize = 0)
        {
            try
            {
                IEnumerable<Kostenstellenart> kostenstellenarten = await _accService.GetKostenstellenartenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetKostenstellenartenCountAsync();

                var response = new ApiResponseDTO<Kostenstellenart>
                {
                    Items = kostenstellenarten,
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
        public async Task<IActionResult> PostCostCentreType(Kostenstellenart kostenstellenart)
        {
            try
            {
                kostenstellenart.KostenstellenartID = 0;

                Kostenstellenart neueKostenstellenart = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Kostenstellenarten, kostenstellenart, default);

                await _accService.SaveChangesAsync();

                return Ok(neueKostenstellenart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{CostCentreTypeID}")]
        public async Task<IActionResult> GetCostCentreType(int CostCentreTypeID)
        {
            try
            {
                Kostenstellenart kostenstellenart = (await _accService.GetKostenstellenartAsync(CostCentreTypeID)).FirstOrDefault();
                return Ok(kostenstellenart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{CostCentreTypeID}")]
        public async Task<IActionResult> PutCostCentreType(int CostCentreTypeID, Kostenstellenart kostenstellenart)
        {
            try
            {
                Kostenstellenart kostenstellenartUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Kostenstellenarten, CostCentreTypeID, kostenstellenart);

                await _accService.SaveChangesAsync();

                return Ok(kostenstellenartUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{CostCentreTypeID}")]
        public async Task<IActionResult> DeleteCostCentreType(int CostCentreTypeID)
        {
            try
            {
                IEnumerable<Kostenstellenart> kostenstellenart = await _accService.GetKostenstellenartAsync(CostCentreTypeID);
                _accService.RemoveKostenstellenart(kostenstellenart.First());

                await _accService.SaveChangesAsync();

                return Ok(kostenstellenart.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
