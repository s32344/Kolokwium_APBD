namespace Kolokwium.Entities;

public class Responsible
{
    
    public int BatchId { get; set; }
    public SeedlingBatch Batch { get; set; } = null!;

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public string Role { get; set; } = string.Empty;

}