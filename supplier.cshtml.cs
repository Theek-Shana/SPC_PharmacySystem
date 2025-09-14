using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCMain.Model;
namespace SPCMain.Pages
{
   public class SupplierModel : PageModel
    {
        private readonly ILogger<PageModel> _logger;

        public List<Supplier> Supplier = new List<Supplier>();
       

        public SupplierModel(ILogger<PageModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            string supplierUrl = "https://localhost:7167/api/SPCSupplier";
           

            HttpClient client = new HttpClient();

            // Fetch Supplier Data
            HttpResponseMessage supplierResponse = await client.GetAsync(supplierUrl);
            if (supplierResponse.IsSuccessStatusCode)
            {
                Supplier = await supplierResponse.Content.ReadFromJsonAsync<List<Supplier>>();
            }

           

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            
            return Page();
        }
    }
}
