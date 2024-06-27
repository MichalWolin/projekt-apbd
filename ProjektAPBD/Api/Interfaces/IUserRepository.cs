using Api.Models;

namespace Api.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUser(string login);
    Task SaveChanges();
    Task<User?> GetUserByRefreshToken(string refreshToken);
}