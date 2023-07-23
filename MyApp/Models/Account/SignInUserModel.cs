using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Models.Account
{
    public class SignInUserModel: IdentityUser
    {
        [BindProperty]
        public SignInUser SignInUser { get; set; }
    }
}
