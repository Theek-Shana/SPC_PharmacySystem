using SPCMain.Model;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCMain.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly ILogger<OrdersModel> _logger; 
        public List<DrugOrder> drugOrders { get; set; }
      

        // Constructor to inject the logger
        public OrdersModel(ILogger<OrdersModel> logger)
        {
            _logger = logger;
            drugOrders = new List<DrugOrder>();
           
        }

        // Make OnGet asynchronous to fetch drug orders
        public async Task OnGetAsync()
        {
            string drugOrderUrl = "https://localhost:7167/api/DrugOrder";  // API URL to fetch drug order data

            // Initialize HttpClient and fetch data from API
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(drugOrderUrl);

                    // If the request is successful, read the data into the drugOrders list
                    if (response.IsSuccessStatusCode)
                    {
                        drugOrders = await response.Content.ReadFromJsonAsync<List<DrugOrder>>();
                    
                    }
                    else
                    {
                        // Log or handle error if the API request fails
                        _logger.LogError("Error fetching drug order data from API.");
                    }
                }
                catch (Exception ex)
                {
                    // Log error in case of exception
                    _logger.LogError($"Exception occurred: {ex.Message}");
                }
            }
        }
    }
}

