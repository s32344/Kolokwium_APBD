using Kolokwium.Data;
using Kolokwium.DTOs;
using Kolokwium.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Services;

public class NurseryService : INurseryService
{
    private readonly AppDbContext _context;

    public NurseryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NurseryResponseDto?> GetNurseryBatchesAsync(int nurseryId)
    {
        var nursery = await _context.Nurseries
            .Include(n => n.SeedlingBatches)
                .ThenInclude(sb => sb.Species)
            .Include(n => n.SeedlingBatches)
                .ThenInclude(sb => sb.Responsibilities)
                    .ThenInclude(r => r.Employee)
            .FirstOrDefaultAsync(n => n.NurseryId == nurseryId);

        if (nursery == null) return null;

        return new NurseryResponseDto
        {
            NurseryId = nursery.NurseryId,
            Name = nursery.Name,
            EstablishedDate = nursery.EstablishedDate,
            Batches = nursery.SeedlingBatches.Select(sb => new BatchDto
            {
                BatchId = sb.BatchId,
                Quantity = sb.Quantity,
                SownDate = sb.SownDate,
                ReadyDate = sb.ReadyDate,
                Species = new SpeciesDto
                {
                    LatinName = sb.Species.LatinName,
                    GrowthTimeInYears = sb.Species.GrowthTimeInYears
                },
                Responsible = sb.Responsibilities.Select(r => new ResponsibleDto
                {
                    FirstName = r.Employee.FirstName,
                    LastName = r.Employee.LastName,
                    Role = r.Role
                }).ToList()
            }).ToList()
        };
    }

    public async Task<(int StatusCode, string Message)> AddBatchAsync(AddBatchRequestDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var nursery = await _context.Nurseries.FirstOrDefaultAsync(n => n.Name == request.Nursery);
            if (nursery == null) return (404, "Szkółka leśna o podanej nazwie nie istnieje.");

            var species = await _context.TreeSpecies.FirstOrDefaultAsync(s => s.LatinName == request.Species);
            if (species == null) return (404, "Gatunek o podanej nazwie nie istnieje.");

            var employeeIds = request.Responsible.Select(r => r.EmployeeId).Distinct().ToList();
            var existingEmployeesCount = await _context.Employees.CountAsync(e => employeeIds.Contains(e.EmployeeId));
            if (existingEmployeesCount != employeeIds.Count) return (404, "Jeden lub więcej pracowników o podanym ID nie istnieje.");

            var newBatch = new SeedlingBatch
            {
                NurseryId = nursery.NurseryId,
                SpeciesId = species.SpeciesId,
                Quantity = request.Quantity,
                SownDate = DateTime.Now 
            };

            _context.SeedlingBatches.Add(newBatch);
            await _context.SaveChangesAsync();

            foreach (var resp in request.Responsible)
            {
                _context.Responsibles.Add(new Responsible
                {
                    BatchId = newBatch.BatchId,
                    EmployeeId = resp.EmployeeId,
                    Role = resp.Role
                });
            }
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            
            return (201, "Partia została pomyślnie dodana.");
        }
        catch
        {
            await transaction.RollbackAsync();
            return (500, "Wystąpił błąd podczas dodawania partii.");
        }
    }
}