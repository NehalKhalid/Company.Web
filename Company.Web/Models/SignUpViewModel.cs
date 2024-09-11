using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="First Name Is Required !!")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required !!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage ="Invalid Format For Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required !!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$", ErrorMessage = "Password must be at least 6 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [Compare(nameof(Password) , ErrorMessage = "Confirm Password dose not Match Password !!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = " Required To Agree !!")]
        public bool IsAgree { get; set; }
    }
}
