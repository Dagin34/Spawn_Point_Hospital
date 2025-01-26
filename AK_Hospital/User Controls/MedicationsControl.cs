using AK_Hospital.Domain_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AK_Hospital.User_Controls
{
    public partial class MedicationsControl : UserControl
    {
        public MedicationsControl()
        {
            InitializeComponent();
        }

        public int counter = 0;


        private void addMedication_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(medId.Text))
            {
                error1.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(medName.Text))
            {
                error2.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }


            Medications m = new Medications()
            {
                Medicationid = int.Parse(medId.Text),
                MedicationName = medName.Text,
                PrescribedFor = prescribedFor.Text,
            };
            ThirdLayer tl = new ThirdLayer();
            clear.PerformClick();
            tl.saveMedication(m);
        }

        private void updateMedication_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(medId.Text))
            {
                error1.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }
            if (String.IsNullOrEmpty(medName.Text))
            {
                error2.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }


            Medications m = new Medications()
            {
                Medicationid = int.Parse(medId.Text),
                MedicationName = medName.Text,
                PrescribedFor = prescribedFor.Text,
            };
            ThirdLayer tl = new ThirdLayer();
            clear.PerformClick();
            tl.updateMedication(m);
        }

        private void deleteMedication_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(medId.Text))
            {
                error1.Visible = true;
                MessageBox.Show("Please Insert Required Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorDisplay.Start();
                return;
            }


            int send = int.Parse(medId.Text);
            ThirdLayer tl = new ThirdLayer();
            clear.PerformClick();
            tl.deleteMedication(send);
        }

        private void updater_Tick(object sender, EventArgs e)
        {
            ThirdLayer tl = new ThirdLayer();
            dgv.DataSource = tl.getMedication().Tables["Medications"];
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
            }
        }

        private void MedicationsControl_Load(object sender, EventArgs e)
        {
            updater.Start();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            medId.Clear();
            medName.Clear();
            prescribedFor.Clear();
            medId.Focus();
        }

        private void medId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void medId_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(medId.Text))
                return;
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source = Dagmawi\\SQLEXPRESS; Initial Catalog = AKHospital; Integrated Security = true;"))
                {
                    con.Open();
                    int id = int.Parse(medId.Text);

                    SqlCommand cmd = new SqlCommand("select MedicationName from Medication where MedicationId = " + id, con);
                    cmd.CommandType = CommandType.Text;
                    string MedName = (string)cmd.ExecuteScalar();

                    cmd = new SqlCommand("select PrescribedFor from Medication where MedicationId =" + id, con);
                    cmd.CommandType = CommandType.Text;
                    string PreFor = (string)cmd.ExecuteScalar();

                    medName.Text = MedName;
                    prescribedFor.Text = PreFor;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
