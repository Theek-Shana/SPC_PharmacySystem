using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCMain.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace SPCMain.Pages
{
    
    public class IndexModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _client;

        public List<Supplier> Supplier { get; private set; } = new List<Supplier>();
        public List<Tender> Tenders { get; private set; } = new List<Tender>();
        public List<Medicine> LowStockMedicines { get; private set; } = new List<Medicine>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _client = new HttpClient();
        }

        public async Task<IActionResult> OnGet()
        {
            string supplierUrl = "https://localhost:7167/api/SPCSupplier";
            string tenderUrl = "https://localhost:7167/api/SPCTenderRequest";
            string medicineUrl = "https://localhost:7167/api/Medicine"; // Corrected API call

            try
            {
                // Fetch Supplier Data
                HttpResponseMessage supplierResponse = await _client.GetAsync(supplierUrl);
                if (supplierResponse.IsSuccessStatusCode)
                {
                    Supplier = await supplierResponse.Content.ReadFromJsonAsync<List<Supplier>>();
                }

                // Fetch Tender Data
                HttpResponseMessage tenderResponse = await _client.GetAsync(tenderUrl);
                if (tenderResponse.IsSuccessStatusCode)
                {
                    Tenders = await tenderResponse.Content.ReadFromJsonAsync<List<Tender>>();
                }

                // Fetch Medicine Data
                HttpResponseMessage medicineResponse = await _client.GetAsync(medicineUrl);
                if (medicineResponse.IsSuccessStatusCode)
                {
                    var medicines = await medicineResponse.Content.ReadFromJsonAsync<List<Medicine>>();
                    LowStockMedicines = medicines.Where(m => m.Quantity < 5).ToList(); // Filter low-stock medicines
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            return Page();
        }
    }
}
