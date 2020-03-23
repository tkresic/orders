using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Orders
{
    public partial class Orders : Form
    {
        OleDbConnection DbConn;
        DataTable dt;
        String table = "Orders";

        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            loadData();
            dataGridView1.Columns["first_name"].HeaderText = "Name";
            dataGridView1.Columns["last_name"].HeaderText = "Surname";
            dataGridView1.Columns["email"].HeaderText = "Email";
            dataGridView1.Columns["street"].HeaderText = "Street";
            dataGridView1.Columns["house_number"].HeaderText = "House Number";
            dataGridView1.Columns["city"].HeaderText = "City";
            dataGridView1.Columns["postal_code"].HeaderText = "Postal Code";
            dataGridView1.Columns["country"].HeaderText = "Country";
            dataGridView1.Columns["note"].HeaderText = "Note";
            dataGridView1.Columns["item"].HeaderText = "Item";
            dataGridView1.Columns["quantity"].HeaderText = "Quantity";
            dataGridView1.Columns["telephone_number"].HeaderText = "Telephone Number";
            dataGridView1.Columns["status"].HeaderText = "Status";
            dataGridView1.Columns["payment_status"].HeaderText = "Payment Status";
            dataGridView1.Columns["delivery_status"].HeaderText = "Delivery Status";
            dataGridView1.Columns["gross"].HeaderText = "Gross";
            dataGridView1.Columns["net"].HeaderText = "Net";
        }

        // Delete
        private void button3_Click(object sender, EventArgs e)
        {
            // Check for multiple row selection
            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only delete one row at a time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If no row selected
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("You need to choose a row first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Index of the chosen row
            int index = dataGridView1.SelectedRows[0].Index;
            int OrderID = Convert.ToInt32(dataGridView1.Rows[index].Cells["id"].Value);
            string message = "Are you sure you want to delete Order " + OrderID + "?";

            if (MessageBox.Show(message, "Delete Row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DbConn = new OleDbConnection(Database.ConnString);
                DbConn.Open();

                string query = String.Format("DELETE FROM {0} WHERE id=@id", table);
                OleDbCommand cmd = new OleDbCommand(query, DbConn);
                cmd.Parameters.Add("@id", OleDbType.Numeric).Value = OrderID;
                cmd.ExecuteNonQuery();

                DbConn.Close();

                dataGridView1.Rows.RemoveAt(index);
            }
        }

        // Search
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dt);
            // Search query
            DV.RowFilter = string.Format("first_name LIKE '%{0}%' OR last_name LIKE '%{0}%'", textBox1.Text);
            // Filter
            dataGridView1.DataSource = DV;
        }

        // Create
        private void button1_Click(object sender, EventArgs e)
        {
            var order = new Create();
            order.FormClosing += new FormClosingEventHandler(this.OrderFormClosing);
            order.Show();
        }

        // Edit
        private void button2_Click(object sender, EventArgs e)
        {
            // TODO => Method
            // Check for multiple row selection
            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only delete one row at a time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If no row selected
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("You need to choose a row first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get index of the chosen row
            int index = dataGridView1.SelectedRows[0].Index;

            // Get params
            int id = Convert.ToInt32(dataGridView1.Rows[index].Cells["id"].Value);
            string firstName = Convert.ToString(dataGridView1.Rows[index].Cells["first_name"].Value);
            string lastName = Convert.ToString(dataGridView1.Rows[index].Cells["last_name"].Value);
            string email = Convert.ToString(dataGridView1.Rows[index].Cells["email"].Value);
            string street = Convert.ToString(dataGridView1.Rows[index].Cells["street"].Value);
            string houseNumber = Convert.ToString(dataGridView1.Rows[index].Cells["house_number"].Value);
            string city = Convert.ToString(dataGridView1.Rows[index].Cells["city"].Value);
            string postalCode = Convert.ToString(dataGridView1.Rows[index].Cells["postal_code"].Value);
            string country = Convert.ToString(dataGridView1.Rows[index].Cells["country"].Value);
            string note = Convert.ToString(dataGridView1.Rows[index].Cells["note"].Value);
            string item = Convert.ToString(dataGridView1.Rows[index].Cells["item"].Value);
            string quantity = Convert.ToString(dataGridView1.Rows[index].Cells["quantity"].Value);
            string telephoneNumber = Convert.ToString(dataGridView1.Rows[index].Cells["telephone_number"].Value);
            string status = Convert.ToString(dataGridView1.Rows[index].Cells["status"].Value);
            string paymentStatus = Convert.ToString(dataGridView1.Rows[index].Cells["payment_status"].Value);
            string deliveryStatus = Convert.ToString(dataGridView1.Rows[index].Cells["delivery_status"].Value);
            string gross = Convert.ToString(dataGridView1.Rows[index].Cells["gross"].Value);
            string net = Convert.ToString(dataGridView1.Rows[index].Cells["net"].Value);
            var order = new Create("Edit", id, firstName, lastName, email, street, houseNumber, city, postalCode, country, note, item, quantity, telephoneNumber, status, paymentStatus, deliveryStatus, gross, net);
            order.FormClosing += new FormClosingEventHandler(this.OrderFormClosing);
            order.Show();
        }

        // Load data on Create/Edit Form Close
        private void OrderFormClosing(object sender, FormClosingEventArgs e)
        {
            loadData();
        }

        // Load data in the DataGrid
        private void loadData()
        {
            try
            {
                DbConn = new OleDbConnection(Database.ConnString);
                DbConn.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = DbConn;

                string query = String.Format("SELECT * FROM {0} ORDER BY id", table);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                DbConn.Close();

                if (button2.Enabled == false) button2.Enabled = true;
                if (button3.Enabled == false) button3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var info = new Info();
            info.Show();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Search can be done with name or surname.");
        }
    }
}
