using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PharmacyC
{
    public partial class searchDrugs : Form
    {
        private readonly HttpClient client;

        public searchDrugs()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void btnGetAll(object sender, EventArgs e)
        {
            await LoadData();
        }

        // Update this method to be async
        private async Task LoadData()
        {
            string url = "https://localhost:7167/api/Medicine";

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var medicine = await response.Content.ReadAsStringAsync();
                    dgvMedicine.DataSource = null;
                    dgvMedicine.DataSource = (new JavaScriptSerializer()).Deserialize<List<Medicine>>(medicine);
                }
                else
                {
                    MessageBox.Show("Error fetching data: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Trigger LoadData when the button is clicked
            LoadData();
        }
    }
}
