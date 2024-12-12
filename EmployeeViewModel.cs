using System.ComponentModel.DataAnnotations;

namespace AllFunctionalityNetCore.Models.ViewModel
{
    public class EmployeeViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Salary { get; set; }
    }
}
