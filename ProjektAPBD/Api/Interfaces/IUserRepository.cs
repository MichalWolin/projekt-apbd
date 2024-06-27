using Api.Models;

namespace Api.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUser(string login, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
    Task<User?> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken);
}