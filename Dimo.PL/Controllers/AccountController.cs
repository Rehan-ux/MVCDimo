using Dimo.DAL.Models;
using Dimo.PL.Utilities;
using Dimo.PL.ViewModels.ACounts;
using Dimo.PL.Views.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dimo.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
		#region Register
		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid) //server side validation
			{
                var user = new ApplicationUser()
                {

                   // UserName = viewModel.Email.Split("@")[0],
                    UserName= viewModel.UserName,
                    Email=viewModel.Email,
                    IsAgree= viewModel.IsAgree,
                    FirstName= viewModel.FirstName,
                    LastName= viewModel.LastName,

                };
                var result =  userManager.CreateAsync(user, viewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }
            } 
            return View(viewModel);
        }
        #endregion
        #region login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = userManager.FindByEmailAsync(viewModel.Email).Result;
            if(user is not null)
            {
                bool flag = userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if (flag)
                {
                    var Result = signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account Is Not Alloed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is Locked out");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index),"Home");

                }
               
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
                
            }
            return View(viewModel);
        }
        #endregion
        #region SignOut
        public async Task<IActionResult> SignOutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region Forget PassWord
        [HttpGet]
        public IActionResult ForgetPassWord()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPassWordViewModel viewModel )
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByEmailAsync(viewModel.Email).Result;
                if(user is not null)
                { 
                    var Token= userManager.GeneratePasswordResetTokenAsync(user).Result;
                    //baseURL/Account/Restpassword/Rehansabry13@gmail.com/token
                    var ResetPasswordUrl = Url.Action("ResetPassword", "Account",new { email = viewModel.Email, Token },Request.Scheme);
                    //create Email
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordUrl
                    };
                    //send Email
                    EmailSetting.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");


                }
              
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassWord), viewModel);
        }
        [HttpGet]
        public IActionResult CheackYourInbox()
        {
            return View();
        }
        [HttpGet]
        
        public IActionResult ResetPassword(string email , string Token)
        {
            TempData["email"] = email;  
            TempData["token"] = Token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel); 
            string email = TempData["email"] as string ?? string.Empty;
            string Token = TempData["token"] as string ?? string.Empty;
            var User = userManager.FindByEmailAsync(email).Result;
            if (User != null)
            {
              var Result = userManager.ResetPasswordAsync(User, Token, viewModel.Password).Result;
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                    
                else
                {
                    foreach (var item in Result.Errors)
                        ModelState.AddModelError(string.Empty, item.Description);
                }
            }
           return View(nameof(ResetPassword),viewModel);
        }
        #endregion
    }
}
