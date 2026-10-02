
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Razor2web.data;
using Razor2web.models;
using System.ComponentModel.DataAnnotations;

namespace Razor2web.Pages.employees
{
    [IgnoreAntiforgeryToken]
    public class listModel : PageModel
    {
        private readonly appDbContext app1;
        public List<Employee> empList { get; set; }
        public listModel(appDbContext appdb)
        {
            app1 = appdb;
        }
        [BindProperty]
        public Employee e { get; set; }
        public void OnGet()
        {
            empList = app1.Employeest.ToList();

        }
        public JsonResult OnGetEmployeesJson()
        {
            var employees = app1.Employeest.ToList();
            return new JsonResult(employees);
        }
        public JsonResult OnPostEmployee([FromBody] input i)
        {
            Employee e = new Employee();
            if (!string.IsNullOrEmpty(i.startShift))
            {
                e.startShift = TimeOnly.Parse(i.startShift); // Ensure valid time format
            }
            else
            {
                // Handle the error case (e.g., set a default value, log an error, etc.)
                return new JsonResult(new { success = false, message = "Start Shift is required." });
            }

            if (!string.IsNullOrEmpty(i.endShift))
            {
                e.endShift = TimeOnly.Parse(i.endShift); // Ensure valid time format
            }
            else
            {
                // Handle the error case
                return new JsonResult(new { success = false, message = "End Shift is required." });
            }
            var jobExists = app1.jobs.Any(j => j.Id == i.JobId);

            if (!jobExists)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Selected job does not exist."
                });
            }
            e.Name = i.Name;
            e.gender = i.gender;
            e.department = i.department;
            e.gob_description = "";
            e.JobId = i.JobId;


            app1.Employeest.Add(e);
            app1.SaveChanges();

            return new JsonResult(new { success=true });
            ViewData["message"] = "employee has been created sussefully ";
        }
        public IActionResult OnPostDelete([FromBody]int Id)
        {
            Console.WriteLine($"Received ID: {Id}");
            var employee = app1.Employeest.Find(Id);
           
            if (employee == null)
            {
                return new JsonResult(new { success = false, message = "Employee not found" });
            }
            
            // Remove the employee from the database
            app1.Employeest.Remove(employee);
            app1.SaveChanges();
            
            return new JsonResult(new { success = true, message = "Employee deleted successfully" });
        }
        public IActionResult OnPostUpdate([FromBody] UpdatedEmployee emp)
        {
            if (emp == null)
            {
                return BadRequest(new { success = false, message = "Invalid data" });
            }

            // Fetch the original employee by ID
            var existingEmployee = app1.Employeest.Find(emp.Id);
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
            app1.Employeest.Update(existingEmployee);
            app1.SaveChanges();

            return new JsonResult(new { success = true, message = "Employee updated successfully" });
        }
        public class input
        {

            public int Id { get; set; }
            public string Name { get; set; }
                public string gender { get; set; }
                public string department { get; set; }
                public string gob_description { get; set; }
            public string startShift { get; set; }
            public string endShift { get; set; }
            public int JobId { get; set; }


        }

        public JsonResult OnGetJobResult() {
            var jobs = app1.jobs
                   .Select(j => new
                   {
                       id = j.Id,
                       name = j.Name
                   })
                   .ToList(); return new JsonResult(jobs);
        }

    }
}
