using System.ComponentModel.DataAnnotations;

namespace Online__Smart_Learning_System.Models.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="User Name Is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe {  get; set; }
    }
}
