using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCMain.Model;
using System.Net.Http;
using System.Net.Http.Json;

namespace SPCMain.Pages
{
    public class MedicineModel : PageModel
    {
        private readonly ILogger<MedicineModel> _logger;

        public List<Medicine> medicines = new List<Medicine>();
        public List<Medicine> lowStockMedicines = new List<Medicine>();

        // Constructor to inject the logger
        public MedicineModel(ILogger<MedicineModel> logger)
        {
            _logger = logger;
        }

        // Handles GET requests and fetches data
        public async Task<IActionResult> OnGet()
        {
            string medicineUrl = "https://localhost:7167/api/Medicine";  // API URL to fetch medicine data

            // Initialize HttpClient and fetch data from API
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(medicineUrl);

                    // If the request is successful, read the data into the medicines list
                    if (response.IsSuccessStatusCode)
                    {
                        medicines = await response.Content.ReadFromJsonAsync<List<Medicine>>();
                        lowStockMedicines = medicines.Where(m => m.Quantity < 5).ToList();  // Filter low stock items
                    }
                    else
                    {
                        // Log or handle error if the API request fails
                        _logger.LogError("Error fetching medicine data from API.");
                    }
                }
                catch (Exception ex)
                {
                    // Log error in case of exception
                    _logger.LogError($"Exception occurred: {ex.Message}");
                }
            }

            return Page();  // Return to the Razor Page
        }


        // Placeholder for POST request (can be used for handling form submissions, etc.)
        public async Task<IActionResult> OnPost()
        {
            return Page();
        }
    }
}
