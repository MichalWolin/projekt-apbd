using Api.Models;

namespace Api.Interfaces;

public interface ICompanyRepository
{
    Task<Company?> GetCompany(string krs);
}