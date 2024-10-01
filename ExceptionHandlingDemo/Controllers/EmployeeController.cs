using ExceptionHandlingDemo.Data;
using ExceptionHandlingDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExceptionHandlingDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Logger _logger = Logger.GetInstance(); // Singleton Logger

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index - List all employees
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // Create - Display the create form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create - Handle form submission
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            // Check if an employee with the same name already exists
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Name == employee.Name);

            if (existingEmployee != null)
            {
                // If an employee with the same name exists, throw an exception
                throw new Exception($"An employee with the name '{employee.Name}' already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }


        // Edit - Display the edit form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // Edit - Handle form submission
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex); // Log the exception
                return View("Error");
            }
        }

        // Delete - Display the delete confirmation
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        // Delete - Handle the deletion
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
                if (employee == null) return NotFound();

                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex); // Log the exception
                return View("Error");
            }
        }
    }
}
