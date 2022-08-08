using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MlVitrine.ViewModels;

namespace MlVitrine.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SignInManager<IdentityUser>? _signInManager;

        public AccountController(UserManager<IdentityUser>? userManager, SignInManager<IdentityUser>? signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Get Login page
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        // POst Login page
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        Random random = new Random();
                        int status_code = random.Next(10000000, 99999999);
                        Console.WriteLine(user.Id);
                        return RedirectToAction("Index", "Products");
                    }
                        return RedirectToAction("Index", "Products");
                    //var code = await Get("https://auth.mercadolivre.com.br/authorization?response_type=code&client_id=7003305904370662&redirect_uri=https://localhost:7276");
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login !!");
            return View(loginVM);
        }

        [Authorize]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName, Email = registroVM.UserMail};
                var result = await _userManager.CreateAsync(user, registroVM.Password);
                
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar usuario");
                }
            }
            return View(registroVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
