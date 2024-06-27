using Api.Interfaces;
using Api.Models;

namespace ApiTests.TestObjects;

public class FakeCompanyRepository : ICompanyRepository
{
    private ICollection<Company> _companies;

    public FakeCompanyRepository()
    {
        _companies = new List<Company>
        {
            new Company
            {
                CompanyId = 1,
                Name = "Company 1",
                Krs = "1234567890"
            }
        };
    }

    public Task<Company?> GetCompany(string krs, CancellationToken cancellationToken)
    {
        return Task.FromResult(_companies.FirstOrDefault(c => c.Krs.Equals(krs)));
    }
}