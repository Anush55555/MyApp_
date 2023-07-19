using Microsoft.AspNetCore.Mvc;

namespace MyApp.Models.Account
{
    public class SignInUserModel
    {
        [BindProperty]
        public SignInUser SignInUser { get; set; }
    }
}
