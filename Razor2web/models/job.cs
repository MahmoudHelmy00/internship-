using System.ComponentModel.DataAnnotations;

namespace Razor2web.models
{
    public class job
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string department { get; set; }
        public int salary { get; set; }



    }
}
