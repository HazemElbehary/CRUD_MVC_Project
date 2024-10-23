using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class ResetPasswordViewModel
	{
		[EmailAddress]
        public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
        public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmedPassword { get; set; } = null!;
	}
}
