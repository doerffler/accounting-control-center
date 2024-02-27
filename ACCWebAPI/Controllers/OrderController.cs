using ACC.ViewModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACCDataModel.Stammdaten;
using ACCDataModel.DTO;

namespace ACCWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IACCService _accService;

        public OrderController(ILogger<OrderController> logger, IACCService accService)
        {
            _logger = logger;
            _accService = accService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders(int? currentPage, int? pageSize)
        {
            try
            {
                IEnumerable<Auftrag> auftraege = await _accService.GetAuftraegeAsync(default, currentPage, pageSize);
                int totalCount = await _accService.GetAuftraegeCountAsync();

                var response = new ApiResponseDTO<Auftrag>
                {
                    Items = auftraege,
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
        public async Task<IActionResult> PostOrder(Auftrag auftrag)
        {
            try
            {
                auftrag.AuftragID = 0;

                Auftrag neuerAuftrag = await _accService.AddDataToDbSetFromApiAsync(_accService.GetDbContext().Auftraege, auftrag);

                await _accService.SaveChangesAsync();

                return Ok(neuerAuftrag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{OrderID}")]
        public async Task<IActionResult> GetOrder(int OrderID)
        {
            try
            {
                Auftrag auftrag = (await _accService.GetAuftragAsync(OrderID)).FirstOrDefault();
                return Ok(auftrag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{OrderID}")]
        public async Task<IActionResult> PutOrder(int OrderID, Auftrag auftrag)
        {
            try
            {
                Auftrag auftragUpdated = await _accService.UpdateDataToDbSetFromApiAsync(_accService.GetDbContext().Auftraege, OrderID, auftrag);
                
                await _accService.SaveChangesAsync();
                
                return Ok(auftragUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{OrderID}")]
        public async Task<IActionResult> DeleteOrder(int OrderID)
        {
            try
            {
                IEnumerable<Auftrag> auftrag = await _accService.GetAuftragAsync(OrderID);
                _accService.RemoveAuftrag(auftrag.First());

                await _accService.SaveChangesAsync();

                return Ok(auftrag.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
