using Api.Exceptions;
using Api.Interfaces;
using Api.RequestModels;
using Api.Services;
using ApiTests.TestObjects;
using Microsoft.Extensions.Configuration;
using Shouldly;

namespace ApiTests;

public class UserServiceTests
{
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(new FakeUserRepository(), new ConfigurationBuilder().Build());
    }

    [Fact]
    public async Task Should_ThrowException_WhenLoginIsEmpty()
    {
        var command = new LoginRequestDto
        {
            Login = "",
            Password = "user"
        };

        await Should.ThrowAsync<DomainException>
            (_userService.Login(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPasswordIsEmpty()
    {
        var command = new LoginRequestDto
        {
            Login = "user",
            Password = ""
        };

        await Should.ThrowAsync<DomainException>
            (_userService.Login(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenUserDoesNotExist()
    {
        var command = new LoginRequestDto
        {
            Login = "user1",
            Password = "user"
        };

        await Should.ThrowAsync<DomainException>
            (_userService.Login(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPasswordsDoNotMatch()
    {
        var command = new LoginRequestDto
        {
            Login = "user",
            Password = "user1"
        };

        await Should.ThrowAsync<DomainException>
            (_userService.Login(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenUserDoesNotExistRefresh()
    {
        var command = new RefreshTokenDto
        {
            RefreshToken = "iLEtLuCF/L0lkIfnTQEBX8q2HLVui4GkUCX2TEVj0ls=a"
        };

        await Should.ThrowAsync<DomainException>
            (_userService.Refresh(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenTokenIsInvalid()
    {
        var command = new RefreshTokenDto
        {
            RefreshToken = "iLEtLuCF/L0lkIfnTQEBX8q2HLVui4GkUCX2TEVj0las="
        };

        await Should.ThrowAsync<DomainException>
            (_userService.Refresh(command, CancellationToken.None));
    }
}