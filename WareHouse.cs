using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SPC_Warehouse
{
    public partial class WareHouse : Form
    {
        public WareHouse()
        {
            InitializeComponent();
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string url = "https://localhost:7167/api/Medicine/" + id;

            HttpClient client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var Medicine = await response.Content.ReadAsStringAsync();
                    dgvMedicine.DataSource = null;
                    Medicine medicine = (new JavaScriptSerializer()).Deserialize<Medicine>(Medicine);
                    List<Medicine> list = new List<Medicine> { medicine };
                    dgvMedicine.DataSource = list;
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
            string url = "https://localhost:7167/api/Medicine/" + id;
            HttpClient client = new HttpClient();
            Medicine Medicine = new Medicine();
            Medicine.Name = txtName.Text;
            Medicine.Description = txtDes.Text;

            if (Decimal.TryParse(txtPrice.Text, out decimal price))
            {
                Medicine.Price = price;
            }
            else
            {
                MessageBox.Show("Invalid price format");
                return;
            }

            if (int.TryParse(txtQuantity.Text, out int quantity))
            {
                Medicine.Quantity = quantity;
            }
            else
            {
                MessageBox.Show("Invalid quantity format");
                return;
            }

            string info = (new JavaScriptSerializer()).Serialize(Medicine);
            var content = new StringContent(info, UnicodeEncoding.UTF8, "application/json");
            var response = client.PutAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Product updated");
                LoadData();
            }
            else
            {
                MessageBox.Show("Fail to update Product");
            }
        }


        private async void LoadData()
        {
            string url = "https://localhost:7167/api/Medicine";
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var medicineData = await response.Content.ReadAsStringAsync();
                    List<Medicine> medicines = new JavaScriptSerializer().Deserialize<List<Medicine>>(medicineData);
                    dgvMedicine.DataSource = null;
                    dgvMedicine.DataSource = medicines; 
                }
                else
                {
                    MessageBox.Show("Failed to load data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void dgvMedicine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvMedicine_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                txtID.Text = dgvMedicine.Rows[r].Cells[1].Value.ToString();
                txtName.Text = dgvMedicine.Rows[r].Cells[2].Value.ToString();
                txtDes.Text = dgvMedicine.Rows[r].Cells[3].Value.ToString();
                txtQuantity.Text = dgvMedicine.Rows[r].Cells[4].Value.ToString();
                txtPrice.Text = dgvMedicine.Rows[r].Cells[5].Value.ToString();
            }
        }
    }
}
