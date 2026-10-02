using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor2web.data;
using Razor2web.models;
using Razor2web.models.veiwModels;

namespace Razor2web.Pages.employees
{
    public class addModel : PageModel
    {
        private readonly appDbContext _db;

        public addModel(appDbContext appDb)
        {
            _db= appDb;
        }
        [BindProperty]
        public Employee e { get; set; }
        
        public void OnGet()
        {
        }
        [HttpPost]
        public void OnPost( Employee e) 
        {
            var empVeiwmodel = new Employee
            {
                Name = e.Name,
                gender = e.gender,
                gob_description = e.gob_description,
                endShift = e.endShift,
                startShift = e.startShift,
                department = e.department,
            };
            _db.Employeest.Add(empVeiwmodel);
            _db.SaveChanges();
            ViewData["message"] = "employee has been created sussefully ";

        }
        
    }
}
