using Kolokwium.DTOs;
using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BatchesController : ControllerBase
{ 
    
    private readonly INurseryService _nurseryService;

    public BatchesController(INurseryService nurseryService)
    {
        _nurseryService = nurseryService;
        
    }

    [HttpPost] 
    public async Task<IActionResult> AddBatch([FromBody] AddBatchRequestDto request)
    {
        var result = await _nurseryService.AddBatchAsync(request);

        if (result.StatusCode == 404)
            return NotFound(result.Message);

        if (result.StatusCode == 500)
            return StatusCode(500, result.Message);

        return StatusCode(201, result.Message);
    }
}
