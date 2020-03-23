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
    public partial class Create : Form
    {
        OleDbConnection DbConn;
        int id;

        public Create(
            string action = "Create",
            int id = 0,
            string firstName = null,
            string lastName = null,
            string email = null,
            string street = null,
            string houseNumber = null,
            string city = null,
            string postalCode = null,
            string country = null,
            string note = null,
            string item = null,
            string quantity = null,
            string telephoneNumber = null,
            string status = null,
            string paymentStatus = null,
            string deliveryStatus = null,
            string gross = null,
            string net = null
        )
        {
            InitializeComponent();

            this.id = id;

            if (action == "Create") this.Text = "Create new Order";
            else if (action == "Edit") this.Text = "Edit Order " + this.id;
            else return;

            textBox1.Text = firstName;
            textBox2.Text = lastName;
            textBox3.Text = email;
            textBox4.Text = street;
            textBox5.Text = houseNumber;
            textBox6.Text = city;
            textBox7.Text = postalCode;
            textBox8.Text = country;
            textBox9.Text = note;
            textBox10.Text = item;
            textBox11.Text = quantity;
            textBox12.Text = telephoneNumber;
            textBox13.Text = status;
            textBox14.Text = paymentStatus;
            textBox15.Text = deliveryStatus;
            textBox16.Text = gross;
            textBox17.Text = net;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Name is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Surname is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Email is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Street is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("House Number is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("City is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Postal Code is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Country is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Note is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Item is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Quantity is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox12.Text))
            {
                MessageBox.Show("Telephone Number is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox13.Text))
            {
                MessageBox.Show("Status is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox14.Text))
            {
                MessageBox.Show("Payment Status is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox15.Text))
            {
                MessageBox.Show("Delivery Status is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox16.Text))
            {
                MessageBox.Show("Gross is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrWhiteSpace(textBox17.Text))
            {
                MessageBox.Show("Net is required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    DbConn = new OleDbConnection(Database.ConnString);
                    DbConn.Open();

                    string query;

                    if (this.id != 0)
                        query = "UPDATE Orders SET first_name=@first_name, last_name=@last_name, email=@email, street=@street, house_number=@house_number, city=@city, postal_code=@postal_code, country=@country, note=@note, item=@item, quantity=@quantity, telephone_number=@telephone_number, status=@status, payment_status=@payment_status, delivery_status=@delivery_status, gross=@gross, net=@net WHERE id=@id";
                    else
                        query = "INSERT INTO Orders (first_name, last_name, email, street, house_number, city, postal_code, country, note, item, quantity, telephone_number, status, payment_status, delivery_status, gross, net) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    OleDbCommand cmd = new OleDbCommand(query, DbConn);

                    cmd.Parameters.AddWithValue("@first_name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@last_name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@street", textBox4.Text);
                    cmd.Parameters.AddWithValue("@house_number", textBox5.Text);
                    cmd.Parameters.AddWithValue("@city", textBox6.Text);
                    cmd.Parameters.AddWithValue("@postal_code", textBox7.Text);
                    cmd.Parameters.AddWithValue("@country", textBox8.Text);
                    cmd.Parameters.AddWithValue("@note", textBox9.Text);
                    cmd.Parameters.AddWithValue("@item", textBox10.Text);
                    cmd.Parameters.AddWithValue("@quantity", textBox11.Text);
                    cmd.Parameters.AddWithValue("@telephone_number", textBox12.Text);
                    cmd.Parameters.AddWithValue("@status", textBox13.Text);
                    cmd.Parameters.AddWithValue("@payment_status", textBox14.Text);
                    cmd.Parameters.AddWithValue("@delivery_status", textBox15.Text);
                    cmd.Parameters.AddWithValue("@gross", textBox16.Text);
                    cmd.Parameters.AddWithValue("@net", textBox17.Text);
                    if (this.id != 0) cmd.Parameters.Add("@id", OleDbType.Numeric).Value = this.id;
                    int res = cmd.ExecuteNonQuery();

                    DbConn.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
