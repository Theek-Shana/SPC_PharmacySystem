using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCPublicWeb.Pages.Model;

namespace SPCPublicWeb.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]

        public NewSupplier newSupplier{ get; set; }

        public async Task<IActionResult> OnPost()
        {
            string url = "https://localhost:7167/api/SPCSupplier";
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(newSupplier), Encoding.UTF8, "application/json");

            HttpResponseMessage message = await client.PostAsync(url, content);
            if (message.IsSuccessStatusCode)

            {
                TempData["success"] = "Added Sucessfully";
                return RedirectToPage("Login");
            }

            else
            {
                TempData["failure"] = "Fail to Add";
                return RedirectToPage("Index");
            }
        }
    }
}
