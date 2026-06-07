using System.ComponentModel.DataAnnotations;

namespace Kolokwium.DTOs;

public class AddBatchRequestDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public string Species { get; set; } = string.Empty;

    [Required]
    public string Nursery { get; set; } = string.Empty;

    [Required]
    public List<AddResponsibleDto> Responsible { get; set; } = new List<AddResponsibleDto>();
}

public class AddResponsibleDto
{
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public string Role { get; set; } = string.Empty;
}