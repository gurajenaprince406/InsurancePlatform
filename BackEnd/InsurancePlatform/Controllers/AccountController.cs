using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using InsurancePlatform.Models;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public AccountController(UserManager<User> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new User
        {
            Title = model.Title,
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
            PasswordHash = model.Password
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    // TODO: Implement Login with JWT Token Generation
}
