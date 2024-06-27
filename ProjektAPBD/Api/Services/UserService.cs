using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Exceptions;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<TokensDto> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
    {
        EnsureLoginIsNotEmpty(loginRequestDto.Login);
        EnsurePasswordIsNotEmpty(loginRequestDto.Password);

        User? user = await _userRepository.GetUser(loginRequestDto.Login, cancellationToken);
        EnsureUserExists(user, loginRequestDto.Login);

        var dbPasswordHash = user.Password;
        var requestPasswordHash = SecurityHelpers.GetHashedPasswordWithSalt(loginRequestDto.Password, user.Salt);

        EnsurePasswordsMatch(dbPasswordHash, requestPasswordHash);

        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Role, user.Role)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5050",
            audience: "https://localhost:5050",
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExpiration = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges(cancellationToken);

        return new TokensDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };
    }

    public async Task<TokensDto> Refresh(RefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByRefreshToken(refreshTokenDto.RefreshToken, cancellationToken);
        EnsureUserExists(user);

        EnsureTokenIsValid(user.RefreshTokenExpiration);

        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Role, user.Role)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5050",
            audience: "https://localhost:5050",
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExpiration = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges(cancellationToken);

        return new TokensDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken
        };
    }

    private static void EnsureUserExists(User? user, string login)
    {
        if (user is null)
        {
            throw new DomainException($"User with login {login} not found.");
        }
    }

    private static void EnsurePasswordsMatch(string dbPasswordHash, string requestPasswordHash)
    {
        if (dbPasswordHash != requestPasswordHash)
        {
            throw new DomainException("Invalid password.");
        }
    }

    private static void EnsureLoginIsNotEmpty(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            throw new DomainException("Login cannot be empty.");
        }
    }

    private static void EnsurePasswordIsNotEmpty(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new DomainException("Password cannot be empty.");
        }
    }

    private static void EnsureUserExists(User? user)
    {
        if (user is null)
        {
            throw new DomainException("User not found.");
        }
    }

    private static void EnsureTokenIsValid(DateTime refreshTokenExpiration)
    {
        if (refreshTokenExpiration < DateTime.Now)
        {
            throw new DomainException("Refresh token expired.");
        }
    }
}