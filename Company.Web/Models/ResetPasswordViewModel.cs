using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is Required !!")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$", ErrorMessage = "Password must be at least 6 characters long, contain at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfirmPassword Is Required !!")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password dose not Match Password !!")]
		public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
