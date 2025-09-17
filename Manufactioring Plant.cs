using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SPC_ManufactorPlant
{
    public partial class Manufactioring_Plant : Form
    {
        public Manufactioring_Plant()
        {
            InitializeComponent();
        }

        private void Manufactioring_Plant_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string url = "https://localhost:7167/api/Medicine";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                read.Wait();
                var medicine = read.Result;
                dgvMedicine.DataSource = null;
                dgvMedicine.DataSource = (new JavaScriptSerializer()).
                    Deserialize<List<Medicine>>(medicine);
            }
        }
        private void addMedicineToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7167/api/Medicine";
            
            HttpClient client = new HttpClient();
            Medicine medicine = new Medicine();
            medicine.Name = txtName.Text;
            medicine.Description = txtDes.Text;
            medicine.Price = Decimal.Parse(txtPrice.Text);
            medicine.Quantity = int.Parse(txtQuantity.Text);
            string info = (new JavaScriptSerializer()).
                Serialize(medicine);
            var content = new StringContent(info,
                        UnicodeEncoding.UTF8,
                        "application/json");
            var async = client.PostAsync(url, content);
            if (async.Result.IsSuccessStatusCode)
            {
                LoadData();
            }
            else
            {
                MessageBox.Show("Failed to add medicine");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
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


        private void dgvMedicine_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            LoadData();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string url = "https://localhost:7167/api/Medicine/" + id;
            HttpClient client = new HttpClient();

            var response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Product Deleted");
                LoadData();
            }
            else
                MessageBox.Show("Fail to delete Product");
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
