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
    public partial class EditStudent : Form
    {

        string studentId;

        public EditStudent(string Id)
        {
            InitializeComponent();
            studentId = Id;
            loadComboBoxes();
        }

        private void EditStudent_Load(object sender, EventArgs e)
        {
            DataTable dt = DB.selectStudent(Int32.Parse(studentId));
            cbStudentGroup.SelectedText = dt.Rows[0]["StudentGroup"].ToString();
            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
            txtLastName.Text = dt.Rows[0]["LastName"].ToString();
            cbSchool.SelectedValue = dt.Rows[0]["SchoolId"].ToString();
            txtTeacher.Text = dt.Rows[0]["Teacher"].ToString();
            txtClassPeriod.Text = dt.Rows[0]["ClassPeriod"].ToString();
            cbSex.SelectedText = dt.Rows[0]["Sex"].ToString();
            txtGPA.Text = dt.Rows[0]["GPA"].ToString();
            cbMaritalStatus.SelectedValue = dt.Rows[0]["MaritalStatusId"].ToString();
            txtMarriedTo.Text = dt.Rows[0]["MarriedTo"].ToString();
            txtChildrenNumber.Text = dt.Rows[0]["ChildrenNumber"].ToString();
            txtChildSupport.Text = dt.Rows[0]["ChildSupport"].ToString();
            txtMonthlyIncome.Text = dt.Rows[0]["NetMonthlyIncome"].ToString();
            txtStudentLoans.Text = dt.Rows[0]["StudentLoans"].ToString();
            txtCreditScore.Text = dt.Rows[0]["CreditScore"].ToString();
            txtCheckbook.Text = dt.Rows[0]["Checkbook"].ToString();


            DataTable dt2 = DB.selectOccupationClusterId(Int32.Parse(dt.Rows[0]["OccupationId"].ToString()));
            int occupationClusterId = Int32.Parse(dt2.Rows[0]["ClusterId"].ToString());
            cbOccupationCluster.SelectedValue = occupationClusterId;
            cbOccupation.SelectedValue = dt.Rows[0]["OccupationId"].ToString();
        }

        private void loadComboBoxes()
        {
            DataTable dt1 = DB.selectSchools();
            cbSchool.DisplayMember = "SchoolName";
            cbSchool.ValueMember = "SchoolId";
            cbSchool.DataSource = dt1;

            DataTable dt2 = DB.selectClusters();
            cbOccupationCluster.DisplayMember = "ClusterName";
            cbOccupationCluster.ValueMember = "ClusterId";
            cbOccupationCluster.DataSource = dt2;

            DataTable dt3 = DB.selectMaricalStatusIds();
            cbMaritalStatus.DisplayMember = "MaritalStatus";
            cbMaritalStatus.ValueMember = "Id";
            cbMaritalStatus.DataSource = dt3;
        }

        private void cbOccupationCluster_SelectedIndexChanged(object sender, EventArgs e)
        {            
            DataTable dt = DB.selectOccupationsByCluster(cbOccupationCluster.SelectedIndex + 1);
            cbOccupation.DisplayMember = "Occupation";
            cbOccupation.ValueMember = "Id";
            cbOccupation.DataSource = dt;
            cbOccupation.Visible = true;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you absolutely sure you want to delete this student?  If you select yes, this student will be permanently deleted.", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmation.ToString() == "Yes")
            {
                try
                {
                    DB.deleteStudent(Int32.Parse(studentId));
                    MessageBox.Show("The student has been deleted");
                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Could not delete the student.");
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtChildSupport.Text == "")
                txtChildSupport.Text = "0";
            if (txtStudentLoans.Text == "")
                txtStudentLoans.Text = "0";
            if (txtCheckbook.Text == "")
                txtCheckbook.Text = "0";
            
            try
            {
                DB.updateStudent(Int32.Parse(studentId), cbStudentGroup.Text, txtFirstName.Text, txtLastName.Text, Int32.Parse(cbSchool.SelectedValue.ToString()),
                    txtTeacher.Text, txtClassPeriod.Text, cbSex.Text, double.Parse(txtGPA.Text), Int32.Parse(cbOccupation.SelectedValue.ToString()),
                    Int32.Parse(cbMaritalStatus.SelectedValue.ToString()), txtMarriedTo.Text, Int32.Parse(txtChildrenNumber.Text), Int32.Parse(txtChildSupport.Text),
                    Int32.Parse(txtMonthlyIncome.Text), Int32.Parse(txtStudentLoans.Text), Int32.Parse(txtCreditScore.Text), Int32.Parse(txtCheckbook.Text));
                this.Close();
            }
            catch
            {
                MessageBox.Show("There is some invalid data on the form, please make sure all data is valid and try again");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
