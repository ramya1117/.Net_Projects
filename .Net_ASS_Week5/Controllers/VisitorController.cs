using Microsoft.AspNetCore.Mvc;
using VisitorSystem.Model;
using VisitorSystem.Services;

namespace VisitorSystem.Controllers
{
  
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class VisitorController : Controller
        {
            private readonly IVService _vService;

            public VisitorController(IVService visitorService)
            {
                _vService = visitorService;
            }

            [HttpPost]
            public async Task<Visitor> AddVisitor(Visitor visitorModel)
            {
                return await _vService.AddVisitor(visitorModel);
            }

            [HttpGet("{id}")]
            public async Task<Visitor> GetVisitorById(string id)
            {
                return await _vService.GetVisitorById(id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateVisitor(string id, Visitor visitorModel)
            {
                try
                {
                    var updatedVisitor = await _vService.UpdateVisitor(id, visitorModel);
                    return Ok(updatedVisitor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteVisitor(string id)
            {
                await _vService.DeleteVisitor(id);
                return NoContent();
            }

        }
    
}
