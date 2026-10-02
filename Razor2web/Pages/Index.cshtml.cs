using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor2web.data;

namespace Razor2web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly appDbContext _dbContext;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            appDbContext dbContext,
            ILogger<IndexModel> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public JsonResult OnGetEmployeeActivity()
        {
            // Get all employees from the database
            var employees = _dbContext.Employeest.ToList();

            // Create 24 hours: 00:00 -> 23:00
            var activity = Enumerable.Range(0, 24)
                .Select(hour =>
                {
                    // Convert the current hour into minutes
                    // Example:
                    // 09:00 = 540
                    // 10:00 = 600
                    int hourStart = hour * 60;
                    int hourEnd = hourStart + 60;

                    // Count employees whose shift overlaps this hour
                    int activeEmployees = employees.Count(employee =>
                    {
                        // Convert employee start time to minutes
                        int start = employee.startShift.Hour * 60
                                  + employee.startShift.Minute;

                        // Convert employee end time to minutes
                        int end = employee.endShift.Hour * 60
                                + employee.endShift.Minute;

                        // -----------------------------------------
                        // Regular shift
                        // Example: 09:00 -> 17:00
                        // -----------------------------------------
                        if (start < end)
                        {
                            return start < hourEnd &&
                                   end > hourStart;
                        }

                        // -----------------------------------------
                        // Overnight shift
                        // Example: 22:00 -> 06:00
                        // -----------------------------------------
                        if (start > end)
                        {
                            // Part 1:
                            // Start time -> midnight
                            bool beforeMidnight =
                                start < hourEnd &&
                                1440 > hourStart;

                            // Part 2:
                            // Midnight -> end time
                            bool afterMidnight =
                                end > hourStart;

                            return beforeMidnight || afterMidnight;
                        }

                        // -----------------------------------------
                        // Same start and end time
                        // We consider this as no active shift
                        // -----------------------------------------
                        return false;
                    });

                    // Create the object that will be sent to JavaScript
                    return new
                    {
                        hour = $"{hour:D2}:00",
                        count = activeEmployees
                    };
                })
                .ToList();

            // Return the 24 hours as JSON
            return new JsonResult(activity);
        }
    }
}
