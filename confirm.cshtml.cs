using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCMain.Model;

namespace SPCMain.Pages
{
    public class ConfirmModel : PageModel
    {
        [BindProperty]
        public Status Status { get; set; } = new Status();  // Use Status model

        // Get method to retrieve StatusId for a specific tender
        public async Task<IActionResult> OnGet(int id)
        {
            Status.StatusId = id;  // Set the StatusId when the page is loaded
            return Page();
        }

        // Post method to send the confirmation request
        public async Task<IActionResult> OnPost()
        {
            string url = "https://localhost:7167/api/Status";  // API endpoint

            using (HttpClient client = new HttpClient())
            {
                var jsonContent = JsonSerializer.Serialize(Status);  // Serialize Status model
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Confirmation request sent successfully.";
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    TempData["failure"] = $"Failed to send confirmation request. {responseBody}";
                }
            }

            return RedirectToPage("Index1");
        }
    }
}
