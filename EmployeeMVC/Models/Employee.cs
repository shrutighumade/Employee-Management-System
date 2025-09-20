namespace EmployeeMVC.Models;

public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public required string Department { get; set; }
    public decimal Salary { get; set; }
}