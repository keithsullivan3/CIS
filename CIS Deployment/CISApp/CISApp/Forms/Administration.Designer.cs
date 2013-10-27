using CISApp.DLL;
namespace CISApp
{
    partial class Administration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administration));
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnProcessGroup = new System.Windows.Forms.Button();
            this.btnViewGroup = new System.Windows.Forms.Button();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.gvStudents = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddAnOccupation = new System.Windows.Forms.Button();
            this.gvOccupations = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtAddSchool = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblAddSchool = new System.Windows.Forms.Label();
            this.gvSchools = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStudents)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOccupations)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchools)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(439, 445);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1048, 438);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnProcessGroup);
            this.tabPage1.Controls.Add(this.btnViewGroup);
            this.tabPage1.Controls.Add(this.btnDeleteGroup);
            this.tabPage1.Controls.Add(this.lblGroup);
            this.tabPage1.Controls.Add(this.cbGroup);
            this.tabPage1.Controls.Add(this.gvStudents);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1040, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Students";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnProcessGroup
            // 
            this.btnProcessGroup.Location = new System.Drawing.Point(211, 383);
            this.btnProcessGroup.Name = "btnProcessGroup";
            this.btnProcessGroup.Size = new System.Drawing.Size(88, 23);
            this.btnProcessGroup.TabIndex = 5;
            this.btnProcessGroup.Text = "Process Group";
            this.btnProcessGroup.UseVisualStyleBackColor = true;
            this.btnProcessGroup.Click += new System.EventHandler(this.btnProcessGroup_Click);
            // 
            // btnViewGroup
            // 
            this.btnViewGroup.Location = new System.Drawing.Point(331, 383);
            this.btnViewGroup.Name = "btnViewGroup";
            this.btnViewGroup.Size = new System.Drawing.Size(75, 23);
            this.btnViewGroup.TabIndex = 4;
            this.btnViewGroup.Text = "View Group";
            this.btnViewGroup.UseVisualStyleBackColor = true;
            this.btnViewGroup.Click += new System.EventHandler(this.btnViewGroup_Click);
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Location = new System.Drawing.Point(559, 383);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(96, 23);
            this.btnDeleteGroup.TabIndex = 3;
            this.btnDeleteGroup.Text = "Delete Group";
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(22, 25);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(134, 13);
            this.lblGroup.TabIndex = 2;
            this.lblGroup.Text = "Select Students by Group :";
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.cbGroup.Location = new System.Drawing.Point(162, 22);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(65, 21);
            this.cbGroup.TabIndex = 1;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // gvStudents
            // 
            this.gvStudents.AllowUserToAddRows = false;
            this.gvStudents.AllowUserToDeleteRows = false;
            this.gvStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStudents.Location = new System.Drawing.Point(6, 61);
            this.gvStudents.Name = "gvStudents";
            this.gvStudents.ReadOnly = true;
            this.gvStudents.Size = new System.Drawing.Size(1028, 316);
            this.gvStudents.TabIndex = 0;
            this.gvStudents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvStudents_CellDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAddAnOccupation);
            this.tabPage2.Controls.Add(this.gvOccupations);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1040, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Occupations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAddAnOccupation
            // 
            this.btnAddAnOccupation.Location = new System.Drawing.Point(466, 383);
            this.btnAddAnOccupation.Name = "btnAddAnOccupation";
            this.btnAddAnOccupation.Size = new System.Drawing.Size(126, 23);
            this.btnAddAnOccupation.TabIndex = 1;
            this.btnAddAnOccupation.Text = "Add An Occupation";
            this.btnAddAnOccupation.UseVisualStyleBackColor = true;
            this.btnAddAnOccupation.Click += new System.EventHandler(this.btnAddAnOccupation_Click);
            // 
            // gvOccupations
            // 
            this.gvOccupations.AllowUserToAddRows = false;
            this.gvOccupations.AllowUserToDeleteRows = false;
            this.gvOccupations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvOccupations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOccupations.Location = new System.Drawing.Point(4, 7);
            this.gvOccupations.Name = "gvOccupations";
            this.gvOccupations.ReadOnly = true;
            this.gvOccupations.Size = new System.Drawing.Size(1030, 358);
            this.gvOccupations.TabIndex = 0;
            this.gvOccupations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvOccupations_CellDoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtAddSchool);
            this.tabPage3.Controls.Add(this.btnSubmit);
            this.tabPage3.Controls.Add(this.lblAddSchool);
            this.tabPage3.Controls.Add(this.gvSchools);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1040, 412);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Schools";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtAddSchool
            // 
            this.txtAddSchool.Location = new System.Drawing.Point(603, 105);
            this.txtAddSchool.Name = "txtAddSchool";
            this.txtAddSchool.Size = new System.Drawing.Size(157, 20);
            this.txtAddSchool.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(635, 148);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblAddSchool
            // 
            this.lblAddSchool.AutoSize = true;
            this.lblAddSchool.Location = new System.Drawing.Point(488, 79);
            this.lblAddSchool.Name = "lblAddSchool";
            this.lblAddSchool.Size = new System.Drawing.Size(428, 13);
            this.lblAddSchool.TabIndex = 1;
            this.lblAddSchool.Text = "You may add a school by entering the school name into the textbox and clicking \"S" +
    "ubmit\"";
            // 
            // gvSchools
            // 
            this.gvSchools.AllowUserToAddRows = false;
            this.gvSchools.AllowUserToDeleteRows = false;
            this.gvSchools.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSchools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSchools.Location = new System.Drawing.Point(26, 6);
            this.gvSchools.Name = "gvSchools";
            this.gvSchools.ReadOnly = true;
            this.gvSchools.Size = new System.Drawing.Size(375, 400);
            this.gvSchools.TabIndex = 0;
            this.gvSchools.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSchools_CellDoubleClick);
            // 
            // Administration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 478);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Administration";
            this.Text = "Administration";
            this.Load += new System.EventHandler(this.Administration_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStudents)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvOccupations)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchools)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gvOccupations;
        private System.Windows.Forms.Button btnAddAnOccupation;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.DataGridView gvStudents;
        private System.Windows.Forms.Button btnDeleteGroup;
        private System.Windows.Forms.Button btnViewGroup;
        private System.Windows.Forms.Button btnProcessGroup;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView gvSchools;
        private System.Windows.Forms.TextBox txtAddSchool;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblAddSchool;
    }
}