using System.ComponentModel.DataAnnotations;

namespace AllFunctionalityNetCore.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please  Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please  Enter Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please  Enter Mobile")]
        
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public long? Mobile { get; set; }
        [Required(ErrorMessage = "Please  Enter Password")]        
        public string Password { get; set; }
        [Required(ErrorMessage = "Please  Enter ConfirmPassword")]
        [Compare("Password", ErrorMessage = "ConfirmPassword not match")]
        public string ConfirmPassword { get; set; }
       
        public bool IsActive { get; set; }
       
    }
}
