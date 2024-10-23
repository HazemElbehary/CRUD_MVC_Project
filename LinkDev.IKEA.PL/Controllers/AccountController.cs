using LinkDev.IKEA.DAL.Models.Identity;
using LinkDev.IKEA.DAL.Perisistance.Data;
using LinkDev.IKEA.PL.Maill_Settings;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Configure Services

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationDbContext> _logger;
        private readonly MaillSender _maillSender;

        public AccountController
               (
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    ILogger<ApplicationDbContext> logger,
                    MaillSender maillSender
               )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _maillSender = maillSender;
        }

        #endregion

        #region Sign Up

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var a = await _userManager.FindByNameAsync(model.UserName);

            if (a is { })
            {
                ModelState.AddModelError(string.Empty, "This User Name Is Already Exist :(");
                return View(model);
            }

            var user = new ApplicationUser()
            {
                FName = model.FirstName,
                LName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree
            };

            var Result = await _userManager.CreateAsync(user, model.Password);

            if (Result.Errors.Count() > 0)
            {
                foreach (var error in Result.Errors)
                {
                    _logger.LogError(string.Empty, error);
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Sign In

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var User = await _userManager.FindByEmailAsync(model.Email);

            if (User is not { })
            {
                ModelState.AddModelError(string.Empty, "This Email Is Doesn't Exist :(");
                return View(model);
            }

            var CheckPassword = await _userManager.CheckPasswordAsync(User, model.Password);

            if (!CheckPassword)
            {
                ModelState.AddModelError(string.Empty, "Incorrect Password :(");
                return View(model);
            }

            var SignInResult = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, true);

            if (!User.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Please Confirm Your Email :(");
                return View(model);
            }

            if (SignInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Sorry Your Are Forbidden From Our Website :(");
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        #endregion

        #region Sign Out

        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));
        }

        #endregion

        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var User = await _userManager.FindByEmailAsync(model.Email);

            if (User is not { })
            {
                ModelState.AddModelError(string.Empty, "This Email Is Doesn't Exist :(");
                return View(model);
            }


            var ResetPasswordMessage = Url.Action(nameof(ResetPassword), "Account", null, Request.Scheme);


            await _maillSender.SenderEmailAsync(User.Email, "Reset Password", ResetPasswordMessage);

            return Content("Please See You Email :)");
        }

        #endregion

        #region Reset Password Action

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var User = await _userManager.FindByEmailAsync(model.Email);

            if (User is not { })
            {
                ModelState.AddModelError(string.Empty, "This Email Is Doesn't Exist :(");
                return View(model);
            }

            var CheckPassword = await _userManager.CheckPasswordAsync(User, model.Password);

            if (CheckPassword)
            {
                ModelState.AddModelError(string.Empty, "This Is The Old Password :(");
                return View(model);
            }



            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(User);

            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(User, resetToken, model.Password);


            if (!passwordChangeResult.Succeeded)
            {
                foreach (var error in passwordChangeResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            
                return View(model);
            }

            return RedirectToAction(nameof(SignIn));
        }

        #endregion

	}
}