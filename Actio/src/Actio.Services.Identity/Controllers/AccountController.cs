using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController:Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
        => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
