using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using AK_Hospital.Domain_Classes;

namespace AK_Hospital
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        //.. GLOBAL VARIABLES
        SqlConnection con;
        string str = "Data Source=Dagmawi\\SQLEXPRESS;Initial Catalog= AKHospital; Integrated Security = true;";
        public bool is_Visible_HideButton = false;

        private void loginButton_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(str)) 
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("CheckLogin", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Username", username.Text);
                    da.SelectCommand.Parameters.AddWithValue("@Password", password.Text);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    if(dt.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand("Login", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username.Text);


                        cmd.ExecuteNonQuery();

                        MainForm form1 = new MainForm();
                        this.Hide();
                        form1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login credentials","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            if (!is_Visible_HideButton)
            {
                is_Visible_HideButton = true;
                password.PasswordChar = username.PasswordChar;
            }
            else
            {
                is_Visible_HideButton = false;
                password.PasswordChar = '•';
            }
        }
    }
}
