using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor2web.data;
using Razor2web.models;

namespace Razor2web.Pages.employees
{
    [IgnoreAntiforgeryToken]
    public class editModel : PageModel
    {
        private readonly appDbContext _dbContext;

        public editModel(appDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        // Fetch employee data
        public IActionResult OnGet(int id)
        {
            Employee = _dbContext.Employeest.Find(id);

            if (Employee == null)
            {
                return NotFound(); // If employee not found, return 404
            }

            return Page(); // Render the page with the employee's data
        }

        // Update employee data
       
        public IActionResult OnPostUpdate([FromBody]UpdatedEmployee emp)
                    {
            if (emp == null)
            {
                return BadRequest(new { success = false, message = "Invalid data" });
            }

            // Fetch the original employee by ID
            var existingEmployee = _dbContext.Employeest.Find(emp.Id);
            if (existingEmployee == null)
            {
                return NotFound(new { success = false, message = "Employee not found" });
            }

            // Update fields
            existingEmployee.Name = emp.Name;
            existingEmployee.gender = emp.gender;
            existingEmployee.department = emp.department;
            existingEmployee.gob_description = emp.gob_description;
            if (!string.IsNullOrEmpty(emp.startShift))
            {
                existingEmployee.startShift = TimeOnly.Parse(emp.startShift); // Ensure valid time format
            }
            else
            {
                // Handle the error case (e.g., set a default value, log an error, etc.)
                return new JsonResult(new { success = false, message = "Start Shift is required." });
            }

            if (!string.IsNullOrEmpty(emp.endShift))
            {
                existingEmployee.endShift = TimeOnly.Parse(emp.endShift); // Ensure valid time format
            }
            else
            {
                // Handle the error case
                return new JsonResult(new { success = false, message = "End Shift is required." });
            }

            // Save changes
            _dbContext.Employeest.Update(existingEmployee);
            _dbContext.SaveChanges();

            return new JsonResult(new { success = true, message = "Employee updated successfully" });
        }
       
    }

        // Class for incoming employee data
        public class UpdatedEmployee
        {
            public int Id { get;set;}
            public string Name { get; set; }
            public string gender { get; set; }
            public string department { get; set; }
            public string gob_description { get; set; }
            public string startShift { get; set; }
            public string endShift { get; set; }

    }
    
}
