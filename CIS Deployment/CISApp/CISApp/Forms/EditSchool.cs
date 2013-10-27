using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISApp.DLL;

namespace CISApp
{
    public partial class EditSchool : Form
    {
        string schoolId;

        public EditSchool(string Id)
        {
            InitializeComponent();
            schoolId = Id;
        }

        private void EditSchool_Load(object sender, EventArgs e)
        {
            DataTable dt = DB.selectSchool(Int32.Parse(schoolId));
            txtSchoolName.Text = dt.Rows[0]["schoolName"].ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you absolutely sure you want to delete this school?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmation.ToString() == "Yes")
            {
                try
                {
                    DB.deleteSchool(Int32.Parse(schoolId));
                    MessageBox.Show("The school has been deleted");
                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Could not delete the school.");
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DB.updateSchool(Int32.Parse(schoolId), txtSchoolName.Text);
            MessageBox.Show("The school name has been updated");
            this.Close();
        }
    }
}
