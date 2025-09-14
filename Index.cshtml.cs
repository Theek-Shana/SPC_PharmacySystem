using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SPCMain.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        public IActionResult OnPost()
        {
            if (Username == AdminUsername && Password == AdminPassword)
            {
                return RedirectToPage("/Index1");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return Page();
        }
    }
}