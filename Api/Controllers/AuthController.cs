using TestApp.Application.Queries.UserManagement;
using TestApp.Application.Services.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
namespace TestApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            return Ok("Done");
        }
        [HttpGet]
        [Route("current-user")]
        [AllowAnonymous]
        public async Task<ActionResult> GetCurrentUser()
        {
          var result=await _userService.GetApplicationUser();
            return result == null ? NotFound() : Ok(result);
        }
    }
}
