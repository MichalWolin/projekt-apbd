using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class SoftwareRepository : ISoftwareRepository
{
    private readonly DatabaseContext _context;

    public SoftwareRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Software?> GetSoftware(int id)
    {
        return await _context.Software.FirstOrDefaultAsync(s => s.SoftwareId.Equals(id));
    }
}