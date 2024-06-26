using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _context;

    public CompanyRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetCompany(string krs)
    {
        return await _context.Companies.FirstOrDefaultAsync(c => c.Krs.Equals(krs));
    }
}