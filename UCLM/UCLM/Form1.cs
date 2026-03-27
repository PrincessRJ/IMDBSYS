using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCLM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-R5SU7F6\\SQLEXPRESS;Initial Catalog=UCLM;Integrated Security=True;");

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO [dbo].[Students] ([Name], [Age], [Year]) VALUES (@Name, @Age, @Year)", con);

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                cmd.Parameters.AddWithValue("@Year", txtYear.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                this.studentsTableAdapter.Fill(this.uCLMDataSet1.Students);

                txtName.Clear();
                txtAge.Clear();
                txtYear.Clear();

                MessageBox.Show("Data saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.studentsTableAdapter.Fill(this.uCLMDataSet1.Students);
        }
    }
}