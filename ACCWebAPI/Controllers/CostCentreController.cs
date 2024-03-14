using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("cost_centres")]
    public class CostCentreController : ControllerBase
    {
        private readonly ILogger<CostCentreController> _logger;
        private readonly IACCService _accService;

        public CostCentreController(ILogger<CostCentreController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCostCentres(int? currentPage = 0, int? pageSize = 0)
        {
            try
            {
                IEnumerable<Kostenstelle> kostenstellen = await _accService.GetKostenstellenAsync(default, currentPage, pageSize);
                int totalCount = kostenstellen.Count();

                var response = new ApiResponseDTO<Kostenstelle>
                {
                    Items = kostenstellen,
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
        public async Task<IActionResult> PostCostCentre(Kostenstelle kostenstelle)
        {
            try
            {
                kostenstelle.KostenstelleID = 0;

                Kostenstelle neueKostenstelle = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Kostenstellen, kostenstelle, default);

                await _accService.SaveChangesAsync();

                return Ok(neueKostenstelle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{CostCentreID}")]
        public async Task<IActionResult> GetCostCentre(int CostCentreID)
        {
            try
            {
                Kostenstelle kostenstelle = (await _accService.GetKostenstelleAsync(CostCentreID)).FirstOrDefault();
                return Ok(kostenstelle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{CostCentreID}")]
        public async Task<IActionResult> PutCostCentre(int CostCentreID, Kostenstelle kostenstelle)
        {
            try
            {
                Kostenstelle kostenstelleUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Kostenstellen, CostCentreID, kostenstelle);

                await _accService.SaveChangesAsync();

                return Ok(kostenstelleUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{CostCentreID}")]
        public async Task<IActionResult> DeleteCostCentre(int CostCentreID)
        {
            try
            {
                IEnumerable<Kostenstelle> kostenstelle = await _accService.GetKostenstelleAsync(CostCentreID);
                _accService.RemoveKostenstelle(kostenstelle.First());

                await _accService.SaveChangesAsync();

                return Ok(kostenstelle.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
