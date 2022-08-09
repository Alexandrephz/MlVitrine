using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MlVitrine.ViewModels;
using MlVitrine.Services;
using MlVitrine.Data;

namespace MlVitrine.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly SignInManager<IdentityUser>? _signInManager;
        private readonly MlVitrineContext _context;

        public AccountController(UserManager<IdentityUser>? userManager, SignInManager<IdentityUser>? signInManager, MlVitrineContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
                    Random random = new Random();
                    int status_code = random.Next(10000000, 99999999);
                    var HaveCode = new MLCreate(_context).UserHaveCodeMeli(user.UserName);

                    if (HaveCode == true)
                    {
                        var HaveToken = new MLCreate(_context).UserHaveTokenMeli(user.UserName);
                        if (HaveToken == true)
                        {
                            return RedirectToAction("Index", "Products");
                        }
                        else
                        {
                            var SetToken = await new MLCreate(_context).SetUserFirstRequest(status_code, user.UserName);
                            return Redirect(SetToken);
                            var GetCode = new MLCreate(_context).GetUserCode(user.UserName);
                            var GetStatus = new MLCreate(_context).GetCodeStatus(GetCode);
                            var GetToken = await new MLCreate(_context).SetUserCode(user.UserName, GetCode, GetStatus);
                            return RedirectToAction("Index", "Products");
                        }
                    }
                    else
                    {
                        var SetToken = await new MLCreate(_context).SetUserFirstRequest(status_code, user.UserName);
                        return Redirect(SetToken);
                    }
                    

                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login !!");
            return View(loginVM);
        }


        public IActionResult Register()
        {
            return View();
        }

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
