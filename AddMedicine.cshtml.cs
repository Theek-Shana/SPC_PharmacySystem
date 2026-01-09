using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCPublicWeb.Pages.Model;

namespace SPCPublicWeb.Pages
{
    public class AddMedicineModel : PageModel
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
                TempData["success"] = "New Tender Added Successfully";
                return RedirectToPage("SupplierDashboard");
            }
            else
            {
                TempData["failure"] = "Failed to Add Tender";
                return RedirectToPage("SupplierDashboard");
            }
        }
    }
}


