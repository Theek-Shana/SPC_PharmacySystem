using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SPCMain.Model;

namespace SPCMain.Pages
{
    public class TendersModel : PageModel
    {
        private readonly ILogger<TendersModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public Tender Tender { get; set; }

        public TendersModel(ILogger<TendersModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGet(int id)
        {
            string url = "https://localhost:7167/api/SPCTenderRequest/"+ id;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Tender = await response.Content.ReadFromJsonAsync<Tender>();
                }
                else
                {
                    _logger.LogError($"Failed to fetch tender: {response.StatusCode}");
                    ModelState.AddModelError("", "Failed to load tender.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP request failed: {ex.Message}");
                ModelState.AddModelError("", "Error connecting to the server.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEdit()
        {
            string url = "https://localhost:7167/api/SPCTenderRequest";
            var content = new StringContent(JsonSerializer.Serialize(Tender), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Tender updated successfully.";
                return RedirectToPage("Tenders");
            }
            else
            {
                TempData["failure"] = "Failed to update tender.";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            string url = "https://localhost:7167/api/SPCTenderRequest/" + id;

            HttpResponseMessage response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Tender deleted successfully.";
                return RedirectToPage("Tenders");
            }
            else
            {
                TempData["failure"] = "Failed to delete tender.";
                return Page();
            }
        }
    }
}
