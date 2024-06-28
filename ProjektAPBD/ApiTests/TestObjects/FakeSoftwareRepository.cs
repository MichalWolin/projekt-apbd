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
            new Software
            {
                SoftwareId = 1,
                Name = "Software 1",
                Description = "Description 1",
                Version = "1.0",
                Category = "Category 1",
                Price = 100
            },
            new Software
            {
                SoftwareId = 2,
                Name = "Software 2",
                Description = "Description 2",
                Version = "2.0",
                Category = "Category 2",
                Price = 200
            }
        };
    }

    public Task<Software?> GetSoftware(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_software.FirstOrDefault(s => s.SoftwareId.Equals(id)));
    }
}