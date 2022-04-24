using ISSLocation.Models;
using ISSLocation.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISSLocation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssLocationController : Controller
{
    private readonly IIssLocationService _issLocationService;
    
    public IssLocationController(IIssLocationService issLocationService)
    {
        _issLocationService = issLocationService;
    }
    
    [HttpGet("location")]
    public async Task<IssLocation> GetLocations()
    {
        return await _issLocationService.GetLocation();
    }
}