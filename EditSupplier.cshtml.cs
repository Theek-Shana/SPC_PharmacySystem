using SPCMain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SPCMain.Pages
{
    public class EditSupplierModel : PageModel
    {
        private readonly HttpClient _client;

        public EditSupplierModel(HttpClient client)
        {
            _client = client;
        }

        [BindProperty]
        public Supplier Supplier { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            string url = $"https://localhost:7167/api/SPCSupplier/{id}";
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Supplier = JsonSerializer.Deserialize<Supplier>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Supplier == null || Supplier.SupplierId == 0)
            {
                TempData["failure"] = "Invalid supplier details";
                return RedirectToPage("Index");
            }

            string url = $"https://localhost:7167/api/SPCSupplier/{Supplier.SupplierId}";
            var content = new StringContent(JsonSerializer.Serialize(Supplier), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Supplier updated successfully";
            }
            else
            {
                TempData["failure"] = "Failed to update supplier";
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int SupplierId)
        {
            string url = $"https://localhost:7167/api/SPCSupplier/{SupplierId}"; // Use SupplierId directly
            HttpResponseMessage response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Supplier deleted successfully!";
            }
            else
            {
                TempData["failure"] = "Failed to delete supplier.";
            }

            return RedirectToPage("Index");
        }

    }
}
