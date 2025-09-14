using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using SPCMain.Model;


namespace SPCMain.Pages

{
    public class AddNewMedicine : PageModel
    {
        [BindProperty]

        public Medicine Medicine { get; set; }

        public async Task<IActionResult> OnPost()
        {
            string url = "https://localhost:7167/api/Medicine";
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(Medicine), Encoding.UTF8, "application/json");

            HttpResponseMessage message = await client.PostAsync(url, content);
            if (message.IsSuccessStatusCode)

            {
                TempData["success"] = " Added Sucessfully";
                return RedirectToPage("Index1");
            }

            else
            {
                TempData["failure"] = "Fail to Add";
                return RedirectToPage("Index1");
            }
        }
    }
}


