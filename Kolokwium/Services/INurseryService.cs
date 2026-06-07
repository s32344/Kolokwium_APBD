using Kolokwium.DTOs;

namespace Kolokwium.Services;

public interface INurseryService
{
    Task<NurseryResponseDto?> GetNurseryBatchesAsync(int nurseryId);
    Task<(int StatusCode, string Message)> AddBatchAsync(AddBatchRequestDto request);
}