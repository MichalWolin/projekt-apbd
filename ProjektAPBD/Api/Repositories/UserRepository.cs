using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(string login, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken.Equals(refreshToken));
    }
}