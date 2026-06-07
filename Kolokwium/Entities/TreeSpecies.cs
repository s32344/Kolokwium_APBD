namespace Kolokwium.Entities;

public class TreeSpecies
{
    
    public int SpeciesId { get; set; }
    public string LatinName { get; set; } = string.Empty;
    public int GrowthTimeInYears { get; set; }

    public ICollection<SeedlingBatch> SeedlingBatches { get; set; } = new List<SeedlingBatch>();

}