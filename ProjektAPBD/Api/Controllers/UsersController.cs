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
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        return Ok(await _userService.Login(loginRequestDto, cancellationToken));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
    {
        return Ok(await _userService.Refresh(refreshTokenDto, cancellationToken));
    }
}