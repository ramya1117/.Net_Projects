using Microsoft.AspNetCore.Mvc;
using VisitorSystem.Model;
using VisitorSystem.Entity;
using VisitorSystem.Services;

namespace VisitorSystem.Controllers
{
    
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class OfficeUserController : Controller
        {
            private readonly IOService _oService;
            private readonly IVService _vService;


            public OfficeUserController(IOService officeService)
            {
                _oService = officeService;
            }

            [HttpPost]
            public async Task<IActionResult> Login(Login loginModel)
            {
                var officeUser = await _oService.LoginOfficeUser(loginModel.Email, loginModel.Password);
                if (officeUser == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                return Ok(officeUser);
            }

            [HttpGet("{id}")]
            public async Task<Office> GetOfficeById(string id)
            {
                return await _oService.GetOfficeById(id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateOffice(string id, Office securityModel)
            {
                try
                {
                    var updatedSecurity = await _oService.UpdateOffice(id, securityModel);
                    return Ok(updatedSecurity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpPut("{visitorId}")]
            public async Task<IActionResult> UpdateVisitorStatus(string visitorId, bool newStatus)
            {
                try
                {
                    var updatedVisitor = await _vService.UpdateVisitorStatus(visitorId, newStatus);
                    return Ok(updatedVisitor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateVisitorStatus (Controller): {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpGet("{status}")]
            public async Task<IActionResult> GetVisitorsByStatus(bool status)
            {
                var visitors = await _vService.GetVisitorsByStatus(status);
                return Ok(visitors);
            }


            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteOffice(string id)
            {
                await _oService.DeleteOffice(id);
                return NoContent();
            }
        }
    
}
