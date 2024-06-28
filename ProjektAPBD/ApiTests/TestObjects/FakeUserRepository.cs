using Api.Interfaces;
using Api.Models;

namespace ApiTests.TestObjects;

public class FakeUserRepository : IUserRepository
{
    private List<User> _users;

    public FakeUserRepository()
    {
        _users = new List<User>
        {
            new User
            {
                UserId = 1,
                Login = "admin",
                Password = "CrnyJzGKqI2hdJjjj/fVvFF1DE1IxzuqFp8GIPwMSPQ=",
                Salt = "3/VW5z1kkOo+uUFfALuMTQ==",
                Role = "admin",
                RefreshToken = "iLEtLuCF/L0lkIfnTQEBX8q2HLVui4GkUCX2TEVj0ls=",
                RefreshTokenExpiration = DateTime.Now.AddDays(1)
            },
            new User
            {
                UserId = 2,
                Login = "user",
                Password = "NGdQGRBVlul5nqSLofzx3Rs9+nwtkf5h7xoFdsyYtP0=",
                Salt = "5ph7adtYHPSVyTWuYDDmcQ==",
                Role = "user",
                RefreshToken = "+wyC5r2q5ySFDjOnTGG/P8Td/yutHA4aUKQ7mqktW+g=",
                RefreshTokenExpiration = DateTime.Now.AddDays(1)
            },
            new User
            {
                UserId = 1,
                Login = "admin",
                Password = "CrnyJzGKqI2hdJjjj/fVvFF1DE1IxzuqFp8GIPwMSPQ=",
                Salt = "3/VW5z1kkOo+uUFfALuMTQ==",
                Role = "admin",
                RefreshToken = "iLEtLuCF/L0lkIfnTQEBX8q2HLVui4GkUCX2TEVj0las=",
                RefreshTokenExpiration = DateTime.Now.AddDays(-2)
            }
        };
    }

    public Task<User?> GetUser(string login, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Login.Equals(login)));
    }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<User?> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.RefreshToken.Equals(refreshToken)));
    }
}