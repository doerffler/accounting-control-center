using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly IACCService _accService;

        public ActivityController(ILogger<ActivityController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetActivities(int? currentPage = 0, int? pageSize = 0)
        {
            try
            {
                IEnumerable<Taetigkeit> taetigkeiten = await _accService.GetTaetigkeitenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetTaetigkeitenCountAsync();

                var response = new ApiResponseDTO<Taetigkeit>
                {
                    Items = taetigkeiten,
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
        public async Task<IActionResult> PostActivity(Taetigkeit taetigkeit)
        {
            try
            {
                taetigkeit.TaetigkeitID = 0;

                Taetigkeit neueTaetigkeit = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Taetigkeiten, taetigkeit, default);

                await _accService.SaveChangesAsync();

                return Ok(neueTaetigkeit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{ActivityID}")]
        public async Task<IActionResult> GetActivity(int ActivityID)
        {
            try
            {
                Taetigkeit taetigkeit = (await _accService.GetTaetigkeitAsync(ActivityID)).FirstOrDefault();
                return Ok(taetigkeit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{ActivityID}")]
        public async Task<IActionResult> PutActivity(int ActivityID, Taetigkeit taetigkeit)
        {
            try
            {
                Taetigkeit taetigkeitUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Taetigkeiten, ActivityID, taetigkeit);

                await _accService.SaveChangesAsync();

                return Ok(taetigkeitUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{ActivityID}")]
        public async Task<IActionResult> DeleteActivity(int ActivityID)
        {
            try
            {
                IEnumerable<Taetigkeit> taetigkeit = await _accService.GetTaetigkeitAsync(ActivityID);
                _accService.RemoveTaetigkeit(taetigkeit.First());

                await _accService.SaveChangesAsync();

                return Ok(taetigkeit.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
