using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display(Name = "First Name")]
		public string FirstName { get; set; } = null!;

		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;


		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; } = null!;
		

		[EmailAddress]
		public string Email { get; set; } = null!;
		

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		

		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; } = null!;
		
		public bool IsAgree { get; set; }
	}
}
