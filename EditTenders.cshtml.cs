using SPCMain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using System.Reflection;


namespace SPCMain.Pages
{
    public class EditTendersModel : PageModel

    {
        private readonly HttpClient _client;

        public EditTendersModel(HttpClient client)
        {
            _client = client;
        }
        [BindProperty]
        public Tender tender { get; set; }

        public async Task OnGet(int id)
        {
            string url = "https://localhost:7167/api/SPCTenderRequest/" + id;
            HttpClient client = new HttpClient();
            HttpResponseMessage responce = await client.GetAsync(url);
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                tender = JsonSerializer.Deserialize<Tender>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }); ;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            string url = "https://localhost:7167/api/SPCTenderRequest/" + tender.TenderID;
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(Request.Form["btnEdit"]))
            {
                var content = new StringContent(JsonSerializer.Serialize(tender), Encoding.UTF8, "application/json");

                HttpResponseMessage message = await client.PutAsync(url, content);
                if (message.IsSuccessStatusCode)

                {
                    TempData["success"] = "Tender updated Sucessfully";
                    return RedirectToPage("Index");
                }

                else
                {
                    TempData["failure"] = "Fail to update";
                    return RedirectToPage("Index");
                }
            }
            else
            {
                HttpResponseMessage messege = await client.DeleteAsync(url);
                if (messege.IsSuccessStatusCode)

                {
                    TempData["success"] = "Tender Request Deleted Sucessfully";
                    return RedirectToPage("Index");
                }

                else
                {
                    TempData["failure"] = "Fail to delete Request";
                    return RedirectToPage("Index");
                }
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int tenderId)
        {
            string url = $"https://localhost:7167/api/SPCTenderRequest/{tender.TenderID}"; // Use SupplierId directly
            HttpResponseMessage response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = " deleted successfully!";
            }
            else
            {
                TempData["failure"] = "Failed to delete.";
            }

            return RedirectToPage("Index");
        }


    }
}
