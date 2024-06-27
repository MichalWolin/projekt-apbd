using Api.Interfaces;
using Api.Models;

namespace ApiTests.TestObjects;

public class FakePersonRepository : IPersonRepository
{
    private List<Person> _persons;

    public FakePersonRepository()
    {
        _persons = new List<Person>
        {

        };
    }
    public Task<Person?> GetPerson(string pesel, CancellationToken cancellationToken)
    {
        return Task.FromResult(_persons.FirstOrDefault(p => p.Pesel.Equals(pesel)));
    }
}