using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISApp.DLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CISApp
{
    public partial class Administration : Form
    {
        //global variable because EditOccupation form could open 2 different ways
        string str;
        
        public Administration()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //This fills the occupation gridview on form load
        private void Administration_Load(object sender, EventArgs e)
        {
            DataTable dt1 = DB.selectOccupations();
            gvOccupations.DataSource = dt1;
            DataTable dt2 = DB.selectSchools();
            gvSchools.DataSource = dt2;
        }
        //this method enables double clicking on an occupation to edit it.  Global variable 'str' is assigned the value of the first cell in the row that was 
        //double clicked, which is the occupationId. The Id is passed to the constructor of the EditOccupation form.  Also Hides this form and shows operation form.
        private void gvOccupations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            str = gvOccupations.Rows[e.RowIndex].Cells[0].Value.ToString();
            Operation formOperation = new Operation(str);
            formOperation.Show();
            formOperation.FormClosing += formOperation_Closing;
        }
        
        //this method is needed to refresh the occupation gridview after an edit.
        private void formOperation_Closing(object sender, FormClosingEventArgs e)
        {
            DataTable dt = DB.selectOccupations();
            gvOccupations.DataSource = dt;
        }
        //this launches the EditOccupation form.  The global variable 'str' is an emptry string, so the operation form does not get loaded with anything.
        private void btnAddAnOccupation_Click(object sender, EventArgs e)
        {
            str = "";
            Operation formOperation = new Operation(str);
            formOperation.Show();
            formOperation.FormClosing += formOperation_Closing;
        }
        //Launches the EditStudent Form
        private void gvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = gvStudents.Rows[e.RowIndex].Cells[0].Value.ToString();
            EditStudent formEditStudent = new EditStudent(s);
            formEditStudent.Show();
            formEditStudent.FormClosing += formEditStudent_Closing;
        }
        //This method refreshes the student form after an Edit
        private void formEditStudent_Closing(object sender, FormClosingEventArgs e)
        {
            DataTable dt = DB.selectStudents(cbGroup.SelectedItem.ToString());
            gvStudents.DataSource = dt;
        }
        //Launches the EditSchool Form
        private void gvSchools_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = gvSchools.Rows[e.RowIndex].Cells[0].Value.ToString();
            EditSchool formEditSchool = new EditSchool(s);
            formEditSchool.Show();
            formEditSchool.FormClosing += formEditSchool_Closing;
        }
        //This method refreshes the student form after an Edit
        private void formEditSchool_Closing(object sender, FormClosingEventArgs e)
        {
            DataTable dt = DB.selectSchools();
            gvSchools.DataSource = dt;
        }
        //this will update the gridview to display the group currently selected in the combo box
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = DB.selectStudents(cbGroup.SelectedItem.ToString());
            gvStudents.DataSource = dt;
        }
        //this will delete the currently selected group.
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you absolutely sure you want to delete this group?  If you select yes, all the students in the selected group will be deleted and there will be no way to retrieve them.", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmation.ToString() == "Yes")
            {
                try
                {
                    DB.deleteStudents(cbGroup.SelectedItem.ToString());
                    MessageBox.Show("Group " + cbGroup.SelectedItem.ToString() + " has been deleted");
                    DataTable dt = DB.selectStudents(cbGroup.SelectedItem.ToString());
                    gvStudents.DataSource = dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Could not delete the group.");
                }
            }
        }
        //Opens the Group Report form
        private void btnViewGroup_Click(object sender, EventArgs e)
        {
            str = cbGroup.SelectedItem.ToString();
            GroupReport formGroup = new GroupReport(str);
            formGroup.Show();           
        }

        //This method handles marriage and divorce.
        private void btnProcessGroup_Click(object sender, EventArgs e)
        {
            DB.setChildSupportToZero(cbGroup.SelectedItem.ToString());
            createMarriages();
            createDivorces();
            assignCouples();
            assignChildrenToDivorced();
            DB.updateChildrenNumberto0(cbGroup.SelectedItem.ToString());
            DB.setCustodyCheckbook(cbGroup.SelectedItem.ToString());
            DB.setNonCustodyCheckbook(cbGroup.SelectedItem.ToString());
            DataTable dt = DB.selectStudents(cbGroup.SelectedItem.ToString());
            gvStudents.DataSource = dt;
            MessageBox.Show("Processing completed successfully!");
        }

        //this method chooses how many students and which ones to set as married
        private void createMarriages()
        {
            //this gets the number of students
            DataTable dt = DB.selectNumberOfStudents(cbGroup.SelectedItem.ToString());
            int numberOfStudents = Int32.Parse(dt.Rows[0]["studentNo"].ToString());
            //gets the number of males who said "yes" to will you be married
            DataTable dt1 = DB.selectNumberOfMalesYes(cbGroup.SelectedItem.ToString());
            int numberOfMalesYes = Int32.Parse(dt1.Rows[0]["maleYes"].ToString());
            //gets the number of females who said "yes" to will you be married
            DataTable dt2 = DB.selectNumberOfFemalesYes(cbGroup.SelectedItem.ToString());
            int numberOfFemalesYes = Int32.Parse(dt2.Rows[0]["femalesYes"].ToString());
            //this calls a method to determine how many married couples there could possibly be based on the number of males and 
            //females who said they would be married in the class
            int numberOfMarriedCouples = getMaxNumberOfMarriedCouples(numberOfMalesYes, numberOfFemalesYes);
            //this makes sure that the number of married couples is not more then 40% of the class.  The calculation is * .2 because
            //we are going to essentially double the value because we have males and females.
            double maxMarriagesdouble = numberOfStudents * .2;
            int maxMarriages = Convert.ToInt32(maxMarriagesdouble);
            if (numberOfMarriedCouples > maxMarriages)
                numberOfMarriedCouples = maxMarriages;
            //and we finally update marriagestatusId to married without children for males and females
            if (numberOfMarriedCouples > 0)
            {
                DB.updateStudentMaritalStatusMaleMarried(numberOfMarriedCouples, cbGroup.SelectedItem.ToString());
                DB.updateStudentMaritalStatusFemaleMarried(numberOfMarriedCouples, cbGroup.SelectedItem.ToString());
            }
        }
        //This method gets the maximum number of married couples
        private int getMaxNumberOfMarriedCouples(int male, int female)
        {
            if (male >= female)
            {
                return female;
            }
            else
            {
                return male;
            }
        }
        //this method takes whoever was not married and determines which of them remain single and which are divorced
        private void createDivorces()
        {
            //this gets the number of students that are not married
            DataTable dt = DB.selectNumberOfSingleStudents(cbGroup.SelectedItem.ToString());
            int numberOfSingleStudents = Int32.Parse(dt.Rows[0]["single"].ToString());
            //this gets the number of students who will be divorced
            double numberOfDivorcedStudentsdouble = numberOfSingleStudents * .6;
            int numberOfDivorcedStudents = Convert.ToInt32(numberOfDivorcedStudentsdouble);
            //this gets the number of students who indicated on the survey that they would not be married
            DataTable dt1 = DB.selectNumberOfStudentsNo(cbGroup.SelectedItem.ToString());
            int numberOfStudentsNo = Int32.Parse(dt1.Rows[0]["No"].ToString());
            //this if statement is here to make sure that if a student selected that they would not be 
            //married then they will be single.
            if (numberOfStudentsNo < numberOfDivorcedStudents)
            {
                DB.updateStudentMaritalStatusDivorced(numberOfDivorcedStudents, cbGroup.SelectedItem.ToString());
            }
            else
            {
                DB.updateStudentMaritalStatusDivorced(cbGroup.SelectedItem.ToString());
            }
        }
        //this method pairs up the couples and assigns them a number of children
        private void assignCouples()
        {
            //this pairs up the couples.  The male with the highest gpa will get matched with the female with the 
            //highest gpa, and so on down the list, as per request from the client
            DataTable dt1 = DB.selectMarriedMales(cbGroup.SelectedItem.ToString());
            DataTable dt2 = DB.selectMarriedFemales(cbGroup.SelectedItem.ToString());
            for (int x = 0; x < dt1.Rows.Count; x++)
            {
                int id = Int32.Parse(dt2.Rows[x]["StudentId"].ToString());
                string name = (dt1.Rows[x]["FirstName"].ToString() + " " + dt1.Rows[x]["LastName"].ToString());
                int id2 = Int32.Parse(dt1.Rows[x]["StudentId"].ToString());
                string name2 = (dt2.Rows[x]["FirstName"].ToString() + " " + dt2.Rows[x]["LastName"].ToString());
                DB.updateStudentMarriedTo(name, id);
                DB.updateStudentMarriedTo(name2, id2);
            }
            //this gets the number of couples with children, which is half of the married couples rounded up.
            //It then updates the couples that receive children with the average number that the couple desired also rounded up.
            int couplesWithChildren = (int)Math.Ceiling((double)dt1.Rows.Count / 2);
            for (int x = 0; x < couplesWithChildren; x++)
            {
                int maleDesiredChildren = Int32.Parse(dt1.Rows[x]["ChildrenNumber"].ToString());
                int femaleDesiredChildren = Int32.Parse(dt2.Rows[x]["ChildrenNumber"].ToString());
                int averageChildren = (int)Math.Ceiling((double)(maleDesiredChildren + femaleDesiredChildren) / 2);
                DB.updateChildrenNumber(Int32.Parse(dt1.Rows[x]["StudentId"].ToString()), averageChildren);
                DB.updateChildrenNumber(Int32.Parse(dt2.Rows[x]["StudentId"].ToString()), averageChildren);
            }
        }
        //this method calculates the percentage of the divorced people in the class who have children.  It assigns them 
        //a random value between 1 and 3.  It also gets the amount of child support based off the salary of the parent and
        //then sets custody to Yes or No.
        private void assignChildrenToDivorced()
        {
            try
            {
                DataTable dt1 = DB.selectNumberofDivorcedStudentsWithChildren(cbGroup.SelectedItem.ToString());

                double numberOfDivorcedWithChildrenD = Double.Parse(dt1.Rows[0]["number"].ToString());
                int numberOfDivorcedWithChildren = Convert.ToInt32(numberOfDivorcedWithChildrenD);

                DataTable dt2 = DB.selectDivorcedStudentsWithChildren(cbGroup.SelectedItem.ToString(), numberOfDivorcedWithChildren);
                Random rnd = new Random();
                for (int x = 0; x < dt2.Rows.Count; x++)
                {
                    int childrenNumber = rnd.Next(1, 4);
                    int salary = Int32.Parse(dt2.Rows[x]["Salary"].ToString());
                    int childSupport = calculateChildSupport(salary, childrenNumber);
                    DB.updateMaritalStatusDivorcedWithChildren(Int32.Parse(dt2.Rows[x]["StudentId"].ToString()), childrenNumber, childSupport);
                }
                assignCustody();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("***********" +ex + "**************");
            }
        }
        //assigns custody to 25% of divorced with children males, and 75% of divorced with children females
        private void assignCustody()
        {
            DataTable dt1 = DB.selectNumberofDivorcedWithChildrenMales(cbGroup.SelectedItem.ToString());
            DataTable dt2 = DB.selectNumberofDivorcedWithChildrenFemales(cbGroup.SelectedItem.ToString());
            int numberMales = Convert.ToInt32(dt1.Rows[0]["number"].ToString());
            int numberFemales = Convert.ToInt32(dt2.Rows[0]["number"].ToString());
            int numberMalesWithCustody = (int)Math.Round((double)numberMales * .25);
            int numberFemalesWithCustody = (int)Math.Round((double)numberFemales * .75);
            DB.updateMaleCustody(cbGroup.SelectedItem.ToString(), numberMalesWithCustody);
            DB.updateFemaleCustody(cbGroup.SelectedItem.ToString(), numberFemalesWithCustody);
            DB.updateCustodyNo(cbGroup.SelectedItem.ToString());
        }

        //calculates child support
        private int calculateChildSupport(int salary, int childrenNumber)
        {
            int childSupport = 0;
            if (salary < 15001)
            {
                childSupport = 175 + (88 * (childrenNumber - 1));
                return childSupport;
            }
            if (salary > 15000 && salary < 30001)
            {
                childSupport = 300 + (150 * (childrenNumber - 1));
                return childSupport;
            }
            if (salary > 30000 && salary < 60001)
            {
                childSupport = 500 + (250 * (childrenNumber - 1));
                return childSupport;
            }
            if (salary > 60000)
            {
                childSupport = 700 + (350 * (childrenNumber - 1));
                return childSupport;
            }
            return childSupport;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtAddSchool.Text != "")
            {
                DataTable dt1 = DB.selectNextSchoolId();
                int nextSchoolId = Int32.Parse(dt1.Rows[0]["SchoolId"].ToString()) + 1;
                DB.createSchool(nextSchoolId, txtAddSchool.Text);
                DataTable dt2 = DB.selectSchools();
                gvSchools.DataSource = dt2;
                txtAddSchool.Text = "";
            }
        }

    }
}
