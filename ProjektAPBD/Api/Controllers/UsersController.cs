using Api.Interfaces;
using Api.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        return Ok(await _userService.Login(loginRequestDto));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto)
    {
        return Ok(await _userService.Refresh(refreshTokenDto));
    }
}