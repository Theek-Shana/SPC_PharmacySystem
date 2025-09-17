using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json; 

namespace PharmacyC
{
    public partial class SubmitTender : Form
    {
        public SubmitTender()
        {
            InitializeComponent();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://localhost:7167/api/DrugOrder";
                var drugOrder = new
                {
                    id = 0, 
                    name = txtMediName.Text.Trim(),
                    quantity = int.TryParse(txtQuantity.Text.Trim(), out int q) ? q : 0 
                };

                string jsonData = JsonConvert.SerializeObject(drugOrder);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(url, content);

                    MessageBox.Show(response.IsSuccessStatusCode ? "Medicine added!" : "Failed to add.",
                                    "Status", MessageBoxButtons.OK, response.IsSuccessStatusCode ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    txtMediName.Clear();
                    txtQuantity.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Request Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Pharmacy nextForm = new Pharmacy();
            nextForm.Show();
        }
    }
}
