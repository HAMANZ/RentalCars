using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentalCar.DomainLayer.Models;
using RentalCar.ViewModels;

namespace RentalCar.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<EUser> _signInManager;
        private readonly UserManager<EUser> _userManager;

        public AccountController(SignInManager<EUser> signInManager, UserManager<EUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Login (GET)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToLocal(returnUrl);

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        #endregion

        #region Login (POST)
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            model.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || user.Is_deleted)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Best-effort last-login stamp; never block sign-in if it fails.
                try
                {
                    user.LastLoginAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                }
                catch { /* ignore */ }

                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
                ModelState.AddModelError(string.Empty, "This account is locked out. Please try again later.");
            else if (result.IsNotAllowed)
                ModelState.AddModelError(string.Empty, "This account is not allowed to sign in.");
            else
                ModelState.AddModelError(string.Empty, "Invalid email or password.");

            return View(model);
        }
        #endregion

        #region Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Access Denied
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}
