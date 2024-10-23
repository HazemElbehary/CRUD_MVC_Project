using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress]
		public string Email { get; set; } = null!;
    }
}
