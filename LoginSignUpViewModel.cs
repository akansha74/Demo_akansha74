using System.ComponentModel.DataAnnotations;

namespace AllFunctionalityNetCore.Models.ViewModel
{
    public class LoginSignUpViewModel
    {
       
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
       
        [Display(Name ="Remember Me")]
        public bool IsRemember { get; set; }
    }
}
  