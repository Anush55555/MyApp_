using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Models.Account;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace MyApp.Controllers;

public class AccountController : Controller
{
    ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignUp(SignUpUser user)
    {
        _context.UserSignUp.Add(user);
        _context.SaveChanges();
        if (ModelState.IsValid)
            return RedirectToAction("Index","Home");
        return View();
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
   public IActionResult SignIn(SignInUserModel user)
    {
        //System.Threading.Thread.Sleep(200);
        //var checkEmail = _context.UserSignUp.Where(x => x.Email == user.SignInUser.Email).SingleOrDefault();
        //var checkPassword = _context.UserSignUp.Where(y => y.Password == user.SignInUser.Password).SingleOrDefault();
        //if (!ModelState.IsValid || checkEmail == null || checkPassword == null)
        //{
        //    return View("UserNotFound");
        //}
        return RedirectToAction("Index", "Home");
    }

}