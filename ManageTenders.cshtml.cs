using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SPCMain.Model;
using System.Net.Http.Json;

namespace SPCMain.Pages
{
    public class ManageTenderModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Tender> Tenders = new List<Tender>();
        public List<TenderReply> TenderReplies = new List<TenderReply>();


        public ManageTenderModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            string tenderUrl = "https://localhost:7167/api/SPCTenderRequest";
            string tenderReplyUrl = "https://localhost:7167/api/TenderReply";

            using (HttpClient client = new HttpClient())
            {
                // Fetch Tender Data
                HttpResponseMessage tenderResponse = await client.GetAsync(tenderUrl);
                if (tenderResponse.IsSuccessStatusCode)
                {
                    Tenders = await tenderResponse.Content.ReadFromJsonAsync<List<Tender>>();
                }

                // Fetch Tender Replies Data
                HttpResponseMessage tenderReplyResponse = await client.GetAsync(tenderReplyUrl);
                if (tenderReplyResponse.IsSuccessStatusCode)
                {
                    TenderReplies = await tenderReplyResponse.Content.ReadFromJsonAsync<List<TenderReply>>();
                }
            }

            return Page(); 
        }

        public async Task<IActionResult> OnPostDelete(int tenderReplyID)
        {

            string url = $"https://localhost:7167/api/TenderReply/{tenderReplyID}";
            HttpClient client = new HttpClient();
            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                TempData["success"] = "Order deleted successfully!";
            }
            else
            {
                TempData["failure"] = "Failed to delete .";
            }

            return RedirectToPage();
        }



    }
}

