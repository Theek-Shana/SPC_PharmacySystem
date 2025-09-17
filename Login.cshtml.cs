using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCPublicWeb.Pages.Model;
using System.Net.Http.Json;

namespace SPCPublicWeb.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public NewSupplier Supplier { get; set; } = new NewSupplier();

        private readonly ILogger<LoginModel> _logger;
        private readonly HttpClient _httpClient;

        public List<NewSupplier> Suppliers { get; set; } = new List<NewSupplier>();

        public LoginModel(ILogger<LoginModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> OnGet()
        {
            string url = "https://localhost:7167/api/SPCSupplier";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Suppliers = await response.Content.ReadFromJsonAsync<List<NewSupplier>>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Supplier.SupplierEmail))
            {
                ModelState.AddModelError("", "Email is required.");
                return Page();
            }

            // Add a fixed password check
            string fixedPassword = "100";

            string url = "https://localhost:7167/api/SPCSupplier";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Suppliers = await response.Content.ReadFromJsonAsync<List<NewSupplier>>();

                // Check if the email exists
                var existingUser = Suppliers.FirstOrDefault(s => s.SupplierEmail == Supplier.SupplierEmail);

                if (existingUser != null && Request.Form["password"] == fixedPassword)
                {
                    return RedirectToPage("SupplierDashboard"); // Redirect to the dashboard after successful login
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Unable to connect to the server.");
            }

            return Page();
        }

    }
}
