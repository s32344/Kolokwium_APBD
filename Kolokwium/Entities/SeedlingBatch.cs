namespace Kolokwium.Entities;

public class SeedlingBatch
{
    
    public int BatchId { get; set; }
    public int NurseryId { get; set; }
    public Nursery Nursery { get; set; } = null!;
    public int SpeciesId { get; set; }
    public TreeSpecies Species { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }

    public ICollection<Responsible> Responsibilities { get; set; } = new List<Responsible>();

}