using Api.Interfaces;
using Api.Models;

namespace ApiTests.TestObjects;

public class FakeSoftwareRepository : ISoftwareRepository
{
    private List<Software> _software;

    public FakeSoftwareRepository()
    {
        _software = new List<Software>
        {

        };
    }

    public Task<Software?> GetSoftware(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_software.FirstOrDefault(s => s.SoftwareId.Equals(id)));
    }
}