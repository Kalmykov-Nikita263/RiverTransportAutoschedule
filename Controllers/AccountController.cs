using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Models;

namespace RiverTransportAutoschedule.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel userData, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userData.UserLogin);

            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user, userData.Password, userData.RememberMe, false);

                if (result.Succeeded)
                    return Redirect(returnUrl ?? "/");
            }

            ModelState.AddModelError(nameof(LoginViewModel.UserLogin), "Неправильный логин или пароль");
        }

        return View(userData);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
