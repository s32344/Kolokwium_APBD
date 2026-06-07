namespace Kolokwium.DTOs;

public class NurseryResponseDto
{
    public int NurseryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime EstablishedDate { get; set; }
    public List<BatchDto> Batches { get; set; } = new List<BatchDto>();
}

public class BatchDto
{
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }
    public SpeciesDto Species { get; set; } = null!;
    public List<ResponsibleDto> Responsible { get; set; } = new List<ResponsibleDto>();
}

public class SpeciesDto
{
    public string LatinName { get; set; } = string.Empty;
    public int GrowthTimeInYears { get; set; }
}

public class ResponsibleDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}