using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetPerson(string pesel);
}