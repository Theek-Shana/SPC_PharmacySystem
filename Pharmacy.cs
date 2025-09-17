using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace PharmacyC
{
    public partial class Pharmacy : Form
    {
        public Pharmacy()
        {
            InitializeComponent();
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7167/api/PharmacyDrugs";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var medicine = await response.Content.ReadAsStringAsync();
                dgvMedicine.DataSource = null;
                dgvMedicine.DataSource = (new JavaScriptSerializer()).Deserialize<List<PharmacyDrugs>>(medicine);
            }
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string url = "https://localhost:7167/api/PharmacyDrugs/" + id;

            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var Medicine = await response.Content.ReadAsStringAsync();
                    dgvMedicine.DataSource = null;
                    PharmacyDrugs medicine = (new JavaScriptSerializer()).Deserialize<PharmacyDrugs>(Medicine);
                    List<PharmacyDrugs> list = new List<PharmacyDrugs> { medicine };
                    dgvMedicine.DataSource = list;
                }
                else
                {
                    MessageBox.Show("No data found for the given ID.");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Error connecting to the server: " + ex.Message);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string url = "https://localhost:7167/api/PharmacyDrugs/" + id;
            HttpClient client = new HttpClient();

            
            if (string.IsNullOrEmpty(id) || !int.TryParse(id, out _))
            {
                MessageBox.Show("Invalid ID.");
                return;
            }

            PharmacyDrugs medicine = new PharmacyDrugs
            {
                Id = int.Parse(txtID.Text),
                Name = txtName.Text,
                Description = txtDis.Text,
                Price = Decimal.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text)
            };

            string info = (new JavaScriptSerializer()).Serialize(medicine);
            var content = new StringContent(info, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product updated successfully.");
                    LoadData(); 
                }
                else
                {
                    MessageBox.Show("Failed to update the product. Server error: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during update: " + ex.Message);
            }
        }


        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string url = "https://localhost:7167/api/PharmacyDrugs/" + id;
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product deleted successfully.");
                    LoadData(); 
                }
                else
                {
                    MessageBox.Show("Failed to delete the product.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

       

        private void dgvMedicine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0) 
            {
                txtID.Text = dgvMedicine.Rows[r].Cells[1].Value.ToString();
                txtName.Text = dgvMedicine.Rows[r].Cells[2].Value.ToString();
                txtDis.Text = dgvMedicine.Rows[r].Cells[3].Value.ToString();
                txtQuantity.Text = dgvMedicine.Rows[r].Cells[4].Value.ToString();
                txtPrice.Text = dgvMedicine.Rows[r].Cells[5].Value.ToString();
            }
        }
        private async Task LoadData()
        {
            string url = "https://localhost:7167/api/PharmacyDrugs";
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var medicineData = await response.Content.ReadAsStringAsync();
                    List<PharmacyDrugs> medicines = new JavaScriptSerializer().Deserialize<List<PharmacyDrugs>>(medicineData);

                    // Debugging line
                    Console.WriteLine("Data reloaded. Medicines count: " + medicines.Count);

                    dgvMedicine.DataSource = null;
                    dgvMedicine.DataSource = medicines; 
                }
                else
                {
                    MessageBox.Show("Failed to load data. Server error: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubmitTender nextForm = new SubmitTender();
            nextForm.Show();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            searchDrugs nextForm = new searchDrugs();
            nextForm.Show();
        }
    }
}
