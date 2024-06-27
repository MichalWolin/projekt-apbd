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