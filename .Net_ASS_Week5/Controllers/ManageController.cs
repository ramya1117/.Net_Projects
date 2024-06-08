using Microsoft.AspNetCore.Mvc;
using VisitorSystem.Entity;
using VisitorSystem.Model;
using VisitorSystem.Services;

namespace VisitorSystem.Controllers
{
   
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class ManagerController : Controller
        {
            private readonly IMService _mService;
            private readonly IOService _oService;
            private readonly ISService _sService;
            private readonly IVService _vService;

            public ManagerController(IMService managerService, IOService officeService, ISService securityService, IVService visitorService)
            {
                _mService = managerService;
                _oService = officeService;
                _sService = securityService;
                _vService = visitorService;

            }

            [HttpPost]
            
            public async Task<ActionResult<Manager>> AddManager(Manager manager)
            {
                var createdManager = await _mService.AddManager(manager);
                return CreatedAtAction(nameof(GetManagerById), new { id = createdManager.Id }, createdManager);
            }

            [HttpGet("{id}")]
            public async Task<Manager> GetManagerById(string id)
            {
                return await _mService.GetManagerById(id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateManager(string id, Manager managerModel)
            {

                try
                {
                    var updatedManager = await _mService.UpdateManager(id, managerModel);
                    return Ok(updatedManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteManager(string id)
            {
                await _mService.DeleteManager(id);
                return NoContent();
            }
            [HttpPost]
            public async Task<IActionResult> AddOfficeUser(Office officeModel)
            {
                var office = await _oService.AddOffice(officeModel);
                return Ok(office);
            }

            [HttpPost]
            public async Task<IActionResult> AddSecurityUser(Security securityModel)
            {
                var security = await _sService.AddSecurity(securityModel);
                return Ok(security);
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

        }
    
}
