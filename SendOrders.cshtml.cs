using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCMain.Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SPCMain.Pages
{
    public class SendOrdersModel : PageModel
    {
        private readonly ILogger<SendOrdersModel> _logger;

        // Declare PharmacyDrugs object to hold form data and bind it to the form
        [BindProperty]
        public PharmacyDrugs drug { get; set; }

        // Constructor to inject the logger
        public SendOrdersModel(ILogger<SendOrdersModel> logger)
        {
            _logger = logger;
        }

        // Method to send PharmacyDrug data to the API when the form is posted
        public async Task<IActionResult> OnPostAsync()
        {
            if (drug == null)
            {
                _logger.LogError("Drug data is null.");
                return Page(); // Stay on the same page
            }

            string drugOrderUrl = "https://localhost:7167/api/PharmacyDrugs";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Sending a POST request to the API with the drug data
                    HttpResponseMessage response = await client.PostAsJsonAsync(drugOrderUrl, drug);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Drug data sent successfully.");
                        return RedirectToPage("Success"); // Redirect to a success page or handle success
                    }
                    else
                    {
                        _logger.LogError("Failed to send drug data.");
                        return Page(); // Stay on the same page or handle failure
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred: {ex.Message}");
                    return Page(); // Stay on the same page in case of failure
                }
            }
        }
    }
}
