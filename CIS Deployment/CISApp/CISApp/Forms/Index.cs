using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CISApp
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        //hides the current form and shows form selected by button
        private void btnSurveyForm_Click(object sender, EventArgs e)
        {
            Survey formSurvey = new Survey();
            formSurvey.Show();
            this.Hide();
            formSurvey.FormClosing += formSurvey_Closing; 
        }

        private void formSurvey_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void btnAdministration_Click(object sender, EventArgs e)
        {
            Administration formAdministration = new Administration();
            formAdministration.Show();
            this.Hide();
            formAdministration.FormClosing += formAdministration_Closing;
        }

        private void formAdministration_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        //end of showing and hiding forms
                
    }
}
