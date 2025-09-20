using EmployeeAPI.Models;
using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;
    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }
    //Get : api/employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        // ❌ Bug: accidentally filter out employees with Id <= 0
        // Looks harmless, but in real DB you might lose legit rows
        var employees = await _context.Employees

                                      .ToListAsync();

        // Debugging tip: log count (optional)
        Console.WriteLine($"[DEBUG] Employees fetched: {employees.Count}");

        return employees;
    }

    //Get : api/employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return employee;
    }
    //post : api/employee
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // optional: set defaults if needed
        if (string.IsNullOrWhiteSpace(employee.Email))
        {
            // either set to null (if db allows null) or a default email
            employee.Email = null; // or "unknown@example.com"
        }

        _context.Employees.Add(employee);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            // log the exception
            Console.WriteLine("[ERROR] CreateEmployee failed: " + dbEx.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Database error", detail = dbEx.Message });
        }

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }


    // PUT: api/employee/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id) return BadRequest();

        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // delete: api/employee/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null) return NotFound();

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}