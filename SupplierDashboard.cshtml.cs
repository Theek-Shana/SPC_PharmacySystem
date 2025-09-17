using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCPublicWeb.Pages.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SPCPublicWeb.Pages
{
    public class SupplierDashboadModel : PageModel
    {
        private readonly ILogger<SupplierDashboadModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public TenderReply TenderReply { get; set; }

        public List<Tenders> Tenders { get; set; } = new List<Tenders>();
        public List<TenderReply> TenderReplies { get; set; } = new List<TenderReply>();

        public SupplierDashboadModel(ILogger<SupplierDashboadModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string url = "https://localhost:7167/api/SPCTenderRequest";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Tenders = await response.Content.ReadFromJsonAsync<List<Tenders>>();
            }

            return Page();


        }

        public async Task<IActionResult> OnPost()
        {
            string url = "https://localhost:7167/api/TenderReply"; // Corrected URL for POST
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(TenderReply, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }), Encoding.UTF8, "application/json");

            HttpResponseMessage message = await client.PostAsync(url, content);
            string responseContent = await message.Content.ReadAsStringAsync(); // For debugging
            _logger.LogInformation("Response: " + responseContent); // Log the response for better diagnostics

            if (message.IsSuccessStatusCode)
            {
                TempData["success"] = "Added Successfully";
                return RedirectToPage("SupplierDashboard");
            }
            else
            {
                TempData["failure"] = "Failed to Add";
                return RedirectToPage("SupplierDashboard");
            }
        }

    }
      
}

    
