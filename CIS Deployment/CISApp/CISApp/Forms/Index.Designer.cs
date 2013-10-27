namespace CISApp
{
    partial class Index
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Index));
            this.btnSurveyForm = new System.Windows.Forms.Button();
            this.lblCIS = new System.Windows.Forms.Label();
            this.btnAdministration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSurveyForm
            // 
            this.btnSurveyForm.Location = new System.Drawing.Point(201, 183);
            this.btnSurveyForm.Name = "btnSurveyForm";
            this.btnSurveyForm.Size = new System.Drawing.Size(97, 23);
            this.btnSurveyForm.TabIndex = 0;
            this.btnSurveyForm.Text = "Enter Surveys";
            this.btnSurveyForm.UseVisualStyleBackColor = true;
            this.btnSurveyForm.Click += new System.EventHandler(this.btnSurveyForm_Click);
            // 
            // lblCIS
            // 
            this.lblCIS.AutoSize = true;
            this.lblCIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCIS.Location = new System.Drawing.Point(140, 35);
            this.lblCIS.Name = "lblCIS";
            this.lblCIS.Size = new System.Drawing.Size(235, 24);
            this.lblCIS.TabIndex = 1;
            this.lblCIS.Text = "Communities In Schools";
            // 
            // btnAdministration
            // 
            this.btnAdministration.Location = new System.Drawing.Point(201, 237);
            this.btnAdministration.Name = "btnAdministration";
            this.btnAdministration.Size = new System.Drawing.Size(97, 23);
            this.btnAdministration.TabIndex = 2;
            this.btnAdministration.Text = "Administration";
            this.btnAdministration.UseVisualStyleBackColor = true;
            this.btnAdministration.Click += new System.EventHandler(this.btnAdministration_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 388);
            this.Controls.Add(this.btnAdministration);
            this.Controls.Add(this.lblCIS);
            this.Controls.Add(this.btnSurveyForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Index";
            this.Text = "Community In Schools";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSurveyForm;
        private System.Windows.Forms.Label lblCIS;
        private System.Windows.Forms.Button btnAdministration;
    }
}