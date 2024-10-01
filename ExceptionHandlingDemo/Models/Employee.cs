using System.ComponentModel.DataAnnotations;

namespace ExceptionHandlingDemo.Models
{
    public class Employee
    {
        public int Id { get; set; } // Primary key, Identity

        [Required]
        public string Name { get; set; }
        public string JobDescription { get; set; }
        public string Number { get; set; }
        public string Department { get; set; }
    }

}
