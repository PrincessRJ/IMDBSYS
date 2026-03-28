using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UCLM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=CISCOLAB0044;Initial Catalog=UCLM;Integrated Security=True;");

        int selectedId = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.studentsTableAdapter.Fill(this.uCLMDataSet1.Students);
            btnSave.Text = "Save"; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                SqlCommand cmd;
                con.Open();

                if (selectedId == 0) 
                {
                    cmd = new SqlCommand(
                        "INSERT INTO [dbo].[Students] ([Name], [Age], [Year]) VALUES (@Name, @Age, @Year)", con);

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully!");
                }
                else 
                {
                    cmd = new SqlCommand(
                        "UPDATE [dbo].[Students] SET [Name]=@Name, [Age]=@Age, [Year]=@Year WHERE [Id]=@Id", con);

                    cmd.Parameters.AddWithValue("@Id", selectedId);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data updated successfully!");
                }

                con.Close();

                this.studentsTableAdapter.Fill(this.uCLMDataSet1.Students);

                selectedId = 0;
                txtName.Clear();
                txtAge.Clear();
                txtYear.Clear();
                btnSave.Text = "Save"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM [dbo].[Students] WHERE [Id]=@Id", con);

                cmd.Parameters.AddWithValue("@Id", selectedId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                this.studentsTableAdapter.Fill(this.uCLMDataSet1.Students);

                MessageBox.Show("Data deleted successfully!");

                selectedId = 0;
                txtName.Clear();
                txtAge.Clear();
                txtYear.Clear();
                btnSave.Text = "Save"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtYear.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                btnSave.Text = "Update"; 
            }
        }
    }
}
