using AK_Hospital.Domain_Classes;
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
using System.Xml.Linq;

namespace AK_Hospital.User_Controls
{
    public partial class PatientsControl : UserControl
    {
        public PatientsControl()
        {
            InitializeComponent();
        }

        public int counter = 0;

        private void addPatient_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientId.Text))
            {
                error1.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(firstname.Text))
            {
                error2.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(lastname.Text))
            {
                error3.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(age.Text))
            {
                error4.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }



            string status = "Active";
            if (inactive.Checked)
            {
                status = "Inactive";
            }

            Patients p = new Patients()
            {
                PatientId = int.Parse(patientId.Text),
                FirstName = firstname.Text,
                LastName = lastname.Text,
                Age = int.Parse(age.Text),
                Description = description.Text,
                PhoneNumber = number.Text,
                Status = status,
            };

            ThirdLayer tl = new ThirdLayer();
            clear.PerformClick();
            tl.savePatient(p);
        }

        private void updatePatients_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientId.Text))
            {
                error1.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(firstname.Text))
            {
                error2.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(lastname.Text))
            {
                error3.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(age.Text))
            {
                error4.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }



            string status = "Active";
            if (inactive.Checked)
            {
                status = "Inactive";
            }

            Patients p = new Patients()
            {
                PatientId = int.Parse(patientId.Text),
                FirstName = firstname.Text,
                LastName = lastname.Text,
                Age = int.Parse(age.Text),
                Description = description.Text,
                PhoneNumber = number.Text,
                Status = status,
            };

            ThirdLayer tl = new ThirdLayer();
            clear.PerformClick();
            tl.updatePatient(p);
        }

        private void inactiveatePatient_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientId.Text))
            {
                error1.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }



            string status = "Inactive";
            if (active.Checked)
            {
                status = "Active";
            }

            ThirdLayer tl = new ThirdLayer();
            tl.activatePatient((int.Parse(patientId.Text)),status);
            clear.PerformClick();
        }

        private void updater_Tick(object sender, EventArgs e)
        {
            if (allRecords.Checked)
            {
                ThirdLayer tl = new ThirdLayer();
                dgv.DataSource = tl.getPatients().Tables["Patient"];
            }
            else if (inactiveRecords.Checked)
            {
                ThirdLayer tl = new ThirdLayer();
                dgv.DataSource = tl.getInactivePatients().Tables["InactivePatient"];
            }
            else if (activeRecords.Checked)
            {
                ThirdLayer tl = new ThirdLayer();
                dgv.DataSource = tl.getActivePatients().Tables["ActivePatient"];
            }
            
        }

        private void PatientsControl_Load(object sender, EventArgs e)
        {
            updater.Start();
        }

        private void errorDisplay_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 1)
            {
                counter = 0;
                errorDisplay.Stop();
                error1.Visible = false;
                error2.Visible = false;
                error3.Visible = false;
                error4.Visible = false;
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            patientId.Clear();
            firstname.Clear();
            lastname.Clear();
            age.Clear();
            description.Clear();
            number.Clear();
            active.Checked = false;
            inactive.Checked = false;
            patientId.Focus();
        }

        private void IdAndAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '+'))
            {
                e.Handled = true;
            }
        }

        private void patientId_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patientId.Text))
                return;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source = Dagmawi\\SQLEXPRESS; Initial Catalog = AKHospital; Integrated Security = true;"))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter("select FirstName from Patient where PatientId = " + int.Parse(patientId.Text), con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if(dt.Rows.Count == 0)
                    {
                        firstname.Clear();
                        lastname.Clear();
                        age.Clear();
                        description.Clear();
                        number.Clear();
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("select FirstName from Patient where PatientId = " + int.Parse(patientId.Text), con);
                    cmd.CommandType = CommandType.Text;
                    firstname.Text = (string)cmd.ExecuteScalar();

                    cmd = new SqlCommand("select LastName from Patient where PatientId = " + int.Parse(patientId.Text), con);
                    cmd.CommandType = CommandType.Text;
                    lastname.Text = (string)cmd.ExecuteScalar();

                    cmd = new SqlCommand("select Age from Patient where PatientId = " + int.Parse(patientId.Text), con);
                    cmd.CommandType = CommandType.Text;
                    int Age = (int)cmd.ExecuteScalar();
                    age.Text = Age.ToString();

                    cmd = new SqlCommand("select Description from Patient where PatientId = " + int.Parse(patientId.Text), con);
                    cmd.CommandType = CommandType.Text;
                    description.Text = (string)cmd.ExecuteScalar();

                    cmd = new SqlCommand("select PhoneNumber from Patient where PatientId = " + int.Parse(patientId.Text), con);
                    cmd.CommandType = CommandType.Text;
                    number.Text = (string)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
