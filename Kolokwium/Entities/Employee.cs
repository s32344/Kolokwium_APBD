namespace Kolokwium.Entities;

public class Employee
{
    
   public int EmployeeId { get; set; }
   public string FirstName { get; set; } = string.Empty;
   public string LastName { get; set; } = string.Empty;
   public DateTime HireDate { get; set; }

   public ICollection<Responsible> Responsibilities { get; set; } = new List<Responsible>();
    
}