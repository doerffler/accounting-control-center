using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("projects")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IACCService _accService;

        public ProjectController(ILogger<ProjectController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetProjects(int? currentPage = 0, int? pageSize = 0)
        {
            try
            {
                IEnumerable<Projekt> projekte = await _accService.GetProjekteAsync(default, currentPage, pageSize);
                int totalCount = projekte.Count();

                var response = new ApiResponseDTO<Projekt>
                {
                    Items = projekte,
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
        public async Task<IActionResult> PostProject(Projekt projekt)
        {
            try
            {
                projekt.ProjektID = 0;

                Projekt neuerProjekt = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Projekte, projekt, default);

                await _accService.SaveChangesAsync();

                return Ok(neuerProjekt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{ProjectID}")]
        public async Task<IActionResult> GetProject(int ProjectID)
        {
            try
            {
                Projekt projekt = (await _accService.GetProjektAsync(ProjectID)).FirstOrDefault();
                return Ok(projekt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{ProjectID}")]
        public async Task<IActionResult> PutProject(int ProjectID, Projekt projekt)
        {
            try
            {
                Projekt projektUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Projekte, ProjectID, projekt);

                await _accService.SaveChangesAsync();

                return Ok(projektUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{ProjectID}")]
        public async Task<IActionResult> DeleteProject(int ProjectID)
        {
            try
            {
                IEnumerable<Projekt> projekt = await _accService.GetProjektAsync(ProjectID);
                _accService.RemoveProjekt(projekt.First());

                await _accService.SaveChangesAsync();

                return Ok(projekt.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
