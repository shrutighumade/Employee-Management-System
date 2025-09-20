namespace EmployeeAPI.Models;

public class Employee
{
    public int Id { set; get; }
    public required string Name { set; get; }
    public string? Email { get; set; }
    public required string Department { set; get; }
    public decimal Salary { set; get; }
}
