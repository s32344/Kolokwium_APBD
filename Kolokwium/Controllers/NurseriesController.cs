using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NurseriesController : ControllerBase
{
    private readonly INurseryService _nurseryService;

    public NurseriesController(INurseryService nurseryService)
    {
        _nurseryService = nurseryService;
    }

    [HttpGet("{id:int}/batches")]
    public async Task<IActionResult> GetNurseryBatches(int id)
    {
        var result = await _nurseryService.GetNurseryBatchesAsync(id);

        if (result == null)
            return NotFound($"Szkółka leśna o ID {id} nie istnieje.");

        return Ok(result);
    }
}