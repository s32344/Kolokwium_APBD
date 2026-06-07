namespace Kolokwium.Entities;

public class Nursery
{
    public int NurseryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime EstablishedDate { get; set; }

    public ICollection<SeedlingBatch> SeedlingBatches { get; set; } = new List<SeedlingBatch>();    
}