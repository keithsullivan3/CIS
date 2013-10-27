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
    public partial class Operation : Form
    {
        string occupationId;
        int rowId = 0;
        //this is the constructor which takes in the occupation id.
        public Operation(string id)
        {
            InitializeComponent();
            occupationId = id;
        }
        //this is the page load event.  If occupationId = "" then the add new occupation button was clicked and the appropriate buttons are displayed.
        //If occupationId != "" then an occupation was double clicked on the last form.  The correct buttons are displayed and the textboxes are all 
        //filled with the values returned from the select statement.
        private void Operation_Load(object sender, EventArgs e)
        {
            DataTable dt2 = DB.selectClusters();
            DataRow rRow = dt2.NewRow();
            rRow["ClusterId"] = 0;
            rRow["ClusterName"] = "Select A Cluster for this Occupation";
            dt2.Rows.Add(rRow);
            dt2.DefaultView.Sort = "ClusterId";
            cbCluster.DisplayMember = "ClusterName";
            cbCluster.ValueMember = "ClusterId";
            cbCluster.DataSource = dt2;

            DataTable dt3 = DB.selectEducationLevels();
            DataRow rRow2 = dt3.NewRow();
            rRow2["EducationId"] = 0;
            rRow2["EducationLevel"] = "Select an Education Level";
            dt3.Rows.Add(rRow2);
            dt3.DefaultView.Sort = "EducationId";
            cbRequiredEducationLevel.DisplayMember = "EducationLevel";
            cbRequiredEducationLevel.ValueMember = "EducationId";
            cbRequiredEducationLevel.DataSource = dt3;

            if (occupationId == "")
            {
                btnAddOccupation.Visible = true;
                btnSave.Visible = false;
                btnDelete.Visible = false;
            }
            else
            {
                btnAddOccupation.Visible = false;
                btnSave.Visible = true;
                btnDelete.Visible = true;
                DataTable dt1 = DB.selectOccupation(Int32.Parse(occupationId));
                tbOccupation.Text = dt1.Rows[rowId]["Occupation"].ToString();
                cbCluster.SelectedIndex = Int32.Parse(dt1.Rows[rowId]["ClusterId"].ToString());
                tbAnnualGrossSalary.Text = dt1.Rows[rowId]["AnnualGrossSalary"].ToString();
                tbMonthlyGrossSalary.Text = dt1.Rows[rowId]["MonthlyGrossSalary"].ToString();
                tbMarriedAnnualTaxes.Text = dt1.Rows[rowId]["MarriedAnnualTaxes"].ToString();
                tbMarriedMonthlyTaxes.Text = dt1.Rows[rowId]["MarriedMonthlyTaxes"].ToString();
                tbMarriedAfterTaxes.Text = dt1.Rows[rowId]["MarriedAfterTaxes"].ToString();
                tbSingleAnnualTaxes.Text = dt1.Rows[rowId]["SingleAnnualTaxes"].ToString();
                tbSingleMonthlyTaxes.Text = dt1.Rows[rowId]["SingleMonthlyTaxes"].ToString();
                tbSingleAfterTaxes.Text = dt1.Rows[rowId]["SingleAfterTaxes"].ToString();
                tbStudentLoans.Text = dt1.Rows[rowId]["StudentLoans"].ToString();
                tbRequiredGpa.Text = dt1.Rows[rowId]["RequiredGpa"].ToString();
                cbRequiredEducationLevel.SelectedIndex = Int32.Parse(dt1.Rows[rowId]["RequiredEducationLevelId"].ToString());
            }            
        }
        //this is the delete method.  If you click delete you are asked if you are sure.  If you click yes in the confirmation box then the occupation is 
        //deleted.  If you click no the occupation is not deleted.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you absolutely sure you want to delete this occupation?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmation.ToString() == "Yes")
            {
                try
                {
                    DB.deleteOccupation(Int32.Parse(occupationId));
                    MessageBox.Show("Occupation deleted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Could not remove occupation.  This might occur because a student is still assigned this occupation.  You must remove the student or change their occupation before you may proceed.");
                }
            }
            this.Close();
        }
        //this is the method for adding an occupation.  There is error checking here so that if they enter anything other then numbers a messagebox
        //will pop up telling the user there is some invalid data. If there are no errors then a submit is made to the database with the new occupation.
        //Notice that many of the textboxes listed here are not visible, they are only there to hold values.  Calculations are made to determine taxes.
        private void btnAddOccupation_Click(object sender, EventArgs e)
        {
            int number;
            if (tbAnnualGrossSalary.Text.Contains(","))
                tbAnnualGrossSalary.Text = tbAnnualGrossSalary.Text.Replace(",", "");
            if (int.TryParse(tbAnnualGrossSalary.Text.Trim(), out number))
            {
                tbMonthlyGrossSalary.Text = (Int32.Parse(tbAnnualGrossSalary.Text) / 12).ToString();
                if (Int32.Parse(tbAnnualGrossSalary.Text) < 15001)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .14).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .16).ToString();
                }
                if (Int32.Parse(tbAnnualGrossSalary.Text) > 15000 && Int32.Parse(tbAnnualGrossSalary.Text) <= 30000)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .20).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .22).ToString();
                }
                if (Int32.Parse(tbAnnualGrossSalary.Text) > 30000 && Int32.Parse(tbAnnualGrossSalary.Text) <= 60000)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .28).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .32).ToString();
                }
                if (Int32.Parse(tbAnnualGrossSalary.Text) > 60000)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .34).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .36).ToString();
                }
            }
            else
            {
                tbMonthlyGrossSalary.Text = "";
                tbMarriedAnnualTaxes.Text = "";
                tbSingleAnnualTaxes.Text = "";
            }

            if (int.TryParse(tbSingleAnnualTaxes.Text.Trim(), out number))
            {
                tbSingleMonthlyTaxes.Text = (Int32.Parse(tbSingleAnnualTaxes.Text) / 12).ToString();
            }
            else
            {
                tbSingleMonthlyTaxes.Text = "";
            }

            if (int.TryParse(tbMonthlyGrossSalary.Text.Trim(), out number) && tbSingleMonthlyTaxes.Text != "")
            {
                tbSingleAfterTaxes.Text = (Int32.Parse(tbMonthlyGrossSalary.Text) - (Int32.Parse(tbSingleMonthlyTaxes.Text))).ToString();
            }
            else
            {
                tbSingleAfterTaxes.Text = "";
            }

            if (int.TryParse(tbMarriedAnnualTaxes.Text.Trim(), out number))
            {
                tbMarriedMonthlyTaxes.Text = (Int32.Parse(tbMarriedAnnualTaxes.Text) / 12).ToString();
            }
            else
            {
                tbMarriedMonthlyTaxes.Text = "";
            }

            if (int.TryParse(tbMonthlyGrossSalary.Text.Trim(), out number) && tbMarriedMonthlyTaxes.Text != "")
            {
                tbMarriedAfterTaxes.Text = (Int32.Parse(tbMonthlyGrossSalary.Text) - (Int32.Parse(tbMarriedMonthlyTaxes.Text))).ToString();
            }
            else
            {
                tbMarriedAfterTaxes.Text = "";
            }

            if (int.TryParse(tbStudentLoans.Text.Trim(), out number))
            {
                tbStudentLoans.Text = Int32.Parse(tbStudentLoans.Text).ToString();
            }
            else
            {
                tbStudentLoans.Text = "";
            }
            //here we have the insert.  All of the textbox values are made sure to be filled otherwise an error box will pop up.
            if (tbOccupation.Text != "" && cbCluster.SelectedIndex != 0 && tbAnnualGrossSalary.Text != "" && tbMonthlyGrossSalary.Text != "" && tbMarriedAnnualTaxes.Text != "" && tbMarriedMonthlyTaxes.Text != ""
                && tbMarriedAfterTaxes.Text != "" && tbSingleAnnualTaxes.Text != "" && tbSingleMonthlyTaxes.Text != "" && tbSingleAfterTaxes.Text != "" && tbStudentLoans.Text != "" && tbRequiredGpa.Text != "" 
                && cbRequiredEducationLevel.SelectedIndex != 0)
            {
                DataTable dt = DB.selectNextOccupationId();
                int nextOccupationId = Int32.Parse(dt.Rows[0]["ID"].ToString()) + 1;
                DB.createOccupation(nextOccupationId, tbOccupation.Text, Int32.Parse(cbCluster.SelectedValue.ToString()), Int32.Parse(tbAnnualGrossSalary.Text), Int32.Parse(tbMonthlyGrossSalary.Text),
                    Int32.Parse(tbMarriedAnnualTaxes.Text), Int32.Parse(tbMarriedMonthlyTaxes.Text), Int32.Parse(tbMarriedAfterTaxes.Text),
                    Int32.Parse(tbSingleAnnualTaxes.Text), Int32.Parse(tbSingleMonthlyTaxes.Text), Int32.Parse(tbSingleAfterTaxes.Text), Int32.Parse(tbStudentLoans.Text),
                    double.Parse(tbRequiredGpa.Text), Int32.Parse(cbRequiredEducationLevel.SelectedValue.ToString()));
                MessageBox.Show("Occupation Added");
                this.Close();
            }
            else
            {
                MessageBox.Show("Data Invalid.  Please make sure you entered an Occupation name, and valid values for Annual Gross Salary, Married Annual Taxes, and Single Annual Taxes.");
            }            
        }
        //this method is to submit an update of the current occupation.  It is just like the method above except that it submits an update instead of an insert.
        private void btnSave_Click(object sender, EventArgs e)
        {
            int number;
            if (tbAnnualGrossSalary.Text.Contains(","))
                tbAnnualGrossSalary.Text = tbAnnualGrossSalary.Text.Replace(",", "");
            if (int.TryParse(tbAnnualGrossSalary.Text.Trim(), out number))
            {
                tbMonthlyGrossSalary.Text = (Int32.Parse(tbAnnualGrossSalary.Text) / 12).ToString();
                if (Int32.Parse(tbAnnualGrossSalary.Text) < 15001)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .14).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .16).ToString();
                }
                if (Int32.Parse(tbAnnualGrossSalary.Text) > 15000 && Int32.Parse(tbAnnualGrossSalary.Text) <= 30000)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .20).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .22).ToString();
                }
                if (Int32.Parse(tbAnnualGrossSalary.Text) > 30000 && Int32.Parse(tbAnnualGrossSalary.Text) <= 60000)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .28).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .32).ToString();
                }
                if (Int32.Parse(tbAnnualGrossSalary.Text) > 60000)
                {
                    tbMarriedAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .34).ToString();
                    tbSingleAnnualTaxes.Text = Math.Round(Int32.Parse(tbAnnualGrossSalary.Text) * .36).ToString();
                }
            }
            else
            {
                tbMonthlyGrossSalary.Text = "";
                tbMarriedAnnualTaxes.Text = "";
                tbSingleAnnualTaxes.Text = "";
            }

            if (int.TryParse(tbSingleAnnualTaxes.Text.Trim(), out number))
            {
                tbSingleMonthlyTaxes.Text = (Int32.Parse(tbSingleAnnualTaxes.Text) / 12).ToString();
            }
            else
            {
                tbSingleMonthlyTaxes.Text = "";
            }

            if (int.TryParse(tbMonthlyGrossSalary.Text.Trim(), out number) && tbSingleMonthlyTaxes.Text != "")
            {
                tbSingleAfterTaxes.Text = (Int32.Parse(tbMonthlyGrossSalary.Text) - (Int32.Parse(tbSingleMonthlyTaxes.Text))).ToString();
            }
            else
            {
                tbSingleAfterTaxes.Text = "";
            }

            if (int.TryParse(tbMarriedAnnualTaxes.Text.Trim(), out number))
            {
                tbMarriedMonthlyTaxes.Text = (Int32.Parse(tbMarriedAnnualTaxes.Text) / 12).ToString();
            }
            else
            {
                tbMarriedMonthlyTaxes.Text = "";
            }

            if (int.TryParse(tbMonthlyGrossSalary.Text.Trim(), out number) && tbMarriedMonthlyTaxes.Text != "")
            {
                tbMarriedAfterTaxes.Text = (Int32.Parse(tbMonthlyGrossSalary.Text) - (Int32.Parse(tbMarriedMonthlyTaxes.Text))).ToString();
            }
            else 
            {
                tbMarriedAfterTaxes.Text = "";
            }

            if (int.TryParse(tbStudentLoans.Text.Trim(), out number))
            {
                tbStudentLoans.Text = Int32.Parse(tbStudentLoans.Text).ToString();
            }
            else
            {
                tbStudentLoans.Text = "";
            }
            //and here is the update.  Again, all textboxes are made sure to have a value otherwise an error box will pop up.
            if (tbOccupation.Text != "" && cbCluster.SelectedIndex != 0 && tbAnnualGrossSalary.Text != "" && tbMonthlyGrossSalary.Text != "" && tbMarriedAnnualTaxes.Text != "" && tbMarriedMonthlyTaxes.Text != ""
                && tbMarriedAfterTaxes.Text != "" && tbSingleAnnualTaxes.Text != "" && tbSingleMonthlyTaxes.Text != "" && tbSingleAfterTaxes.Text != "" && tbStudentLoans.Text != "" && tbRequiredGpa.Text != ""
                && cbRequiredEducationLevel.SelectedIndex != 0)
            {
                DB.updateOccupation(Int32.Parse(occupationId), tbOccupation.Text, Int32.Parse(cbCluster.SelectedValue.ToString()), Int32.Parse(tbAnnualGrossSalary.Text), Int32.Parse(tbMonthlyGrossSalary.Text),
                    Int32.Parse(tbMarriedAnnualTaxes.Text), Int32.Parse(tbMarriedMonthlyTaxes.Text), Int32.Parse(tbMarriedAfterTaxes.Text),
                    Int32.Parse(tbSingleAnnualTaxes.Text), Int32.Parse(tbSingleMonthlyTaxes.Text), Int32.Parse(tbSingleAfterTaxes.Text), Int32.Parse(tbStudentLoans.Text),
                    double.Parse(tbRequiredGpa.Text), Int32.Parse(cbRequiredEducationLevel.SelectedValue.ToString()));
                this.Close();
            }
            else
            {
                MessageBox.Show("Data Invalid.  Please make sure you entered an Occupation name, and valid values for Annual Gross Salary, Married Annual Taxes, and Single Annual Taxes.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
