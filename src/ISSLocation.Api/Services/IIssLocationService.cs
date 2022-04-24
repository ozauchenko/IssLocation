using ISSLocation.Models;

namespace ISSLocation.Services;

public interface IIssLocationService
{
    Task<IssLocation> GetLocation();
}