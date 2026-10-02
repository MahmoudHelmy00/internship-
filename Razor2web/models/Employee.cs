using System.ComponentModel.DataAnnotations;

namespace Razor2web.models
{
    public class Employee
    {
        [Key]
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public string department { get; set; }
        public string gob_description { get; set; }
        public TimeOnly startShift { get; set; }
        public TimeOnly endShift { get; set; }
        public int JobId { get; set; }

    }
}
