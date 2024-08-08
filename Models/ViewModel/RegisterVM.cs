using System.ComponentModel.DataAnnotations;

namespace Online__Smart_Learning_System.Models.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string? Email {  get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password",ErrorMessage ="Password Does not Match")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword {  get; set; }
        [Required(ErrorMessage ="Address is Required")]
        [DataType(DataType.MultilineText)]
        public string? Address {  get; set; }
    }
}
