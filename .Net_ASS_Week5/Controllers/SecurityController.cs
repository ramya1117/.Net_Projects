using Microsoft.AspNetCore.Mvc;
using VisitorSystem.Model;
using VisitorSystem.Services;

namespace VisitorSystem.Controllers
{
    public class SecurityController
    {
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class SecurityUserController : Controller
        {
            private readonly ISService _sService;
            private readonly IVService _vService;
            public SecurityUserController(ISService securityService, IVService visitorService)
            {
                _sService = securityService;
                _vService = visitorService;
            }

            [HttpPost]
            public async Task<IActionResult> Login(Login loginModel)
            {
                var securityUser = await _sService.LoginSecurityUser(loginModel.Email, loginModel.Password);
                if (securityUser == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                return Ok(securityUser);
            }

            [HttpGet("{status}")]
            public async Task<IActionResult> GetVisitorsByStatus(bool status)
            {
                var visitors = await _vService.GetVisitorsByStatus(status);
                return Ok(visitors);
            }

            [HttpGet("{id}")]
            public async Task<Visitor> GetVisitorById(string id)
            {
                return await _vService.GetVisitorById(id);
            }



            [HttpGet("{id}")]
            public async Task<Security> GetSecurityById(string id)
            {
                return await _sService.GetSecurityById(id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateSecurity(string id, Security securityModel)
            {
                try
                {
                    var updatedSecurity = await _sService.UpdateSecurity(id, securityModel);
                    return Ok(updatedSecurity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateVisitor (Controller): {ex.Message}");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteSecurity(string id)
            {
                await _sService.DeleteSecurity(id);
                return NoContent();
            }
        }
    }
}

