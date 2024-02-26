using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using ACCDataModel.Stammdaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("accounting_units")]
    public class AccountingUnitController : ControllerBase
    {
        private readonly ILogger<AccountingUnitController> _logger;
        private readonly IACCService _accService;

        public AccountingUnitController(ILogger<AccountingUnitController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(int? currentPage, int? pageSize)
        {
            try
            {
                IEnumerable<Abrechnungseinheit> abrechnungseinheiten = await _accService.GetAbrechnungseinheitenAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetAbrechnungseinheitenCountAsync();

                var response = new ApiResponseDTO<Abrechnungseinheit>
                {
                    Items = abrechnungseinheiten,
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
        public async Task<IActionResult> Store(Abrechnungseinheit abrechnungseinheit)
        {
            try
            {
                abrechnungseinheit.AbrechnungseinheitID = 0;

                Abrechnungseinheit neueAbrechnungseinheit = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Abrechnungseinheiten, abrechnungseinheit, default);

                await _accService.SaveChangesAsync();

                return Ok(neueAbrechnungseinheit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{AccountingUnitID}")]
        public async Task<IActionResult> Show(int AccountingUnitID)
        {
            try
            {
                Abrechnungseinheit abrechnungseinheit = (await _accService.GetAbrechnungseinheitAsync(AccountingUnitID)).FirstOrDefault();
                return Ok(abrechnungseinheit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{AccountingUnitID}")]
        public async Task<IActionResult> Update(int AccountingUnitID, Abrechnungseinheit abrechnungseinheit)
        {
            try
            {
                Abrechnungseinheit abrechnungseinheitUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Abrechnungseinheiten, AccountingUnitID, abrechnungseinheit);

                await _accService.SaveChangesAsync();

                return Ok(abrechnungseinheitUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{AccountingUnitID}")]
        public async Task<IActionResult> Destroy(int AccountingUnitID)
        {
            try
            {
                IEnumerable<Abrechnungseinheit> abrechnungseinheit = await _accService.GetAbrechnungseinheitAsync(AccountingUnitID);
                _accService.RemoveAbrechnungseinheit(abrechnungseinheit.First());

                await _accService.SaveChangesAsync();

                return Ok(abrechnungseinheit.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
