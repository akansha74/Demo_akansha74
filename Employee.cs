using System.ComponentModel.DataAnnotations;

namespace AllFunctionalityNetCore.Models
{
    public class Employee
    {
        [Key]
       
        public int Id { get; set; }
      
        public string Name { get; set; }
       
        public string City { get; set; }
       
        public string State { get; set; }
      
        public string Salary { get; set; }
    }
}
