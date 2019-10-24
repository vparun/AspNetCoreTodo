using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Models.AccountViewModels;


[Authorize]
public class AccountController: Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager)
    {
        _userManager=userManager;
        _signInManager=singInManager;
    }

    [TempData]
    public string ErrorMessage {get;set;}

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string returnUrl=null)
    {
        //clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        ViewData["RetrunUrl"]=returnUrl;
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl=null)
    {
        ViewData["RetrunUrl"]=returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl=null)
    {
        ViewData["ReturnUrl"]=returnUrl;
        if(ModelState.IsValid)
        {
            var user= new ApplicationUser { UserName=model.Email, Email=model.Email};
            var result = await _userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                 
            }
            
        }
        return View(model);
    }

}