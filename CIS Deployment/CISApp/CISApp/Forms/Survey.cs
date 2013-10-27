using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISApp.DLL;

namespace CISApp
{
    public partial class Survey : Form
    {
        public Survey()
        {
            InitializeComponent();
        }

        private void btnCloseSurvey_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Fills combo boxes with data
        private void Survey_Load(object sender, EventArgs e)
        {
            //creates a row in the combo box that displays first and reads "Select A School" and then fills combo box with data from database.
            DataTable dt1 = DB.selectSchools();
            DataRow rRow1 = dt1.NewRow();
            rRow1["SchoolId"] = 0;
            rRow1["SchoolName"] = "Select A School";
            dt1.Rows.Add(rRow1);
            cbSchool.DisplayMember = "SchoolName";
            cbSchool.ValueMember = "SchoolId";
            cbSchool.DataSource = dt1;
            cbSchool.SelectedValue = 0;
            
            //creates a row in the combo box that displays first and reads "Select An Occupation" and then fills combo box with data from database.
            DataTable dt2 = DB.selectClusters();
            DataRow rRow = dt2.NewRow();
            rRow["ClusterId"] = 0;
            rRow["ClusterName"] = "Select An Occupation Cluster";
            dt2.Rows.Add(rRow);
            dt2.DefaultView.Sort = "ClusterId";
            cbCluster.DisplayMember = "ClusterName";
            cbCluster.ValueMember = "ClusterId";
            cbCluster.DataSource = dt2;

            //creates a row in the combo box that displays first and reads "Select a Use" and then fills combo box with data from database.
            DataTable dt3 = DB.selectCreditUse();
            cbCreditCardUse.DisplayMember = "Type";
            cbCreditCardUse.ValueMember = "CreditId";
            cbCreditCardUse.DataSource = dt3;
            cbCreditCardUse.SelectedIndex = 0;
        }

        private void btnSubmitStudent_Click(object sender, EventArgs e)
        {
            //gets the value of male or female radio buttons and sets it to string "maleOrFemale"
            string maleOrFemale = "";
            if (rbMale.Checked)
            {
                maleOrFemale = "Male";
            }
            else
            {
                maleOrFemale = "Female";
            }
            //gets the value of education level radio buttons and sets it to int "educationlvl"
            int educationlvl = 0;
            if (rbHighschool.Checked)
            {
                educationlvl = 1;
            }
            else if (rbOnTheJobTraining.Checked)
            {
                educationlvl = 2;
            }
            else if (rbCommunityCollege.Checked)
            {
                educationlvl = 3;
            }
            else if (rbTechnicalSchool.Checked)
            {
                educationlvl = 4;
            }
            else if (rbBachelorDegree.Checked)
            {
                educationlvl = 5;
            }
            else if (rbCollegeAndGraduateSchool.Checked)
            {
                educationlvl = 6;
            }
            //gets the value of married radio buttons and sets it to string "marriedOrSingle"
            string married = "";
            if (rbYes.Checked)
            {
                married = "Yes";
            }
            else
            {
                married = "No";
            }
            //assigns Yes to the value of children if married = Yes and cbChildren is checked
            string children = "";
            if (cbChildren.Checked && married == "Yes")
            {
                children = "Yes";
            }
            else
            {
                children = "No";
            }
            int childrenNumber = 0;
            if (rbYes.Checked && cbChildren.Checked && cbChildrenNumber.SelectedIndex != -1)
            {
                childrenNumber = Int32.Parse(cbChildrenNumber.SelectedItem.ToString());
            }
            //assigns Yes to credit if cbCreditCards is Checked
            string credit = "";
            if (cbCreditCards.Checked)
            {
                credit = "Yes";
            }
            else
            {
                credit = "No";
            }
            //and finally we create a Student
            if (tbFirstName.Text != "" && tbLastName.Text != "" && cbSchool.SelectedIndex != 0 && tbPeriod.Text != "" && tbTeacher.Text != "" && cbGroup.SelectedIndex != -1 && maleOrFemale != ""
                && tbGPA.Text != "" && educationlvl != 0 && cbCluster.SelectedIndex != 0 && married != "" && children != "" && credit != "")
            {
                Student s1 = new Student(tbFirstName.Text, tbLastName.Text, Int32.Parse(cbSchool.SelectedValue.ToString()), tbPeriod.Text, tbTeacher.Text, cbGroup.SelectedItem.ToString(), maleOrFemale, double.Parse(tbGPA.Text),
                    educationlvl, Int32.Parse(cbOccupation.SelectedValue.ToString()), married, children, childrenNumber, credit, Int32.Parse(cbCreditCardUse.SelectedValue.ToString()));
                s1.display();
                MessageBox.Show("Student added to group " + cbGroup.SelectedItem.ToString());
                this.Close();
                Survey formSurvey = new Survey();
                formSurvey.Show();                
            }
            else
            {
                MessageBox.Show("There is some invalid data on the form.  Please make sure that you have filled in all of the values before you try to submit the student.", "Error");
            }
        }

        private void cbCluster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCluster.SelectedIndex > 0)
            {
                DataTable dt = DB.selectOccupationsByCluster(cbCluster.SelectedIndex);
                cbOccupation.DisplayMember = "Occupation";
                cbOccupation.ValueMember = "Id";
                cbOccupation.DataSource = dt;
                cbOccupation.Visible = true;
            }
            else
            {
                cbOccupation.SelectedIndex = -1;
                cbOccupation.Visible = false;
            }
        }

        private void cbOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOccupation.Visible == true)
            {
                DataTable dt = DB.selectOccupation(Int32.Parse(cbOccupation.SelectedValue.ToString()));
                int salary = Int32.Parse(dt.Rows[0]["annualgrosssalary"].ToString());
                lblSalary.Visible = true;
                lblSalary.Text = "Salary : " + salary.ToString();
                Console.WriteLine(Int32.Parse(cbOccupation.SelectedValue.ToString()));
                Console.WriteLine(salary);
            }
        }

    }
}
