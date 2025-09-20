using EmployeeMVC.Models;
using EmployeeMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _service;
        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var employee = await _service.GetAllAsync();
            return View(employee);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            
            {
                await _service.CreateAsync(emp);
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _service.GetByIdAsync(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(emp);



                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _service.GetByIdAsync(id);
            return View(emp);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}