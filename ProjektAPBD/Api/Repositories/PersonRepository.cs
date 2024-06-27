using Api.Data;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DatabaseContext _context;

    public PersonRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetPerson(string pesel, CancellationToken cancellationToken)
    {
        return await _context.Persons.FirstOrDefaultAsync(p => p.Pesel.Equals(pesel));
    }
}