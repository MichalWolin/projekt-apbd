using Api.Models;

namespace Api.Interfaces;

public interface ISoftwareRepository
{
    Task<Software?> GetSoftware(int id, CancellationToken cancellationToken);
}