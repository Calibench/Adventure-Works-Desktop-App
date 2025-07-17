namespace Adventure_Works_Desktop_App.EmployeePage.Frontend
{
    partial class EditEmployeeInformationForm
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
            this.idLabel = new System.Windows.Forms.Label();
            this.vacationHoursNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.backButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.firstNameGroupBox = new System.Windows.Forms.GroupBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameGroupBox = new System.Windows.Forms.GroupBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.middleNameGroupBox = new System.Windows.Forms.GroupBox();
            this.middleNameTextBox = new System.Windows.Forms.TextBox();
            this.jobTitleGroupBox = new System.Windows.Forms.GroupBox();
            this.jobTitleComboBox = new System.Windows.Forms.ComboBox();
            this.birthDateGroupBox = new System.Windows.Forms.GroupBox();
            this.birthDateTextBox = new System.Windows.Forms.TextBox();
            this.maritalStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.singleRadioButton = new System.Windows.Forms.RadioButton();
            this.marriedRadioButton = new System.Windows.Forms.RadioButton();
            this.genderGroupBox = new System.Windows.Forms.GroupBox();
            this.MaleRadioButton = new System.Windows.Forms.RadioButton();
            this.femaleRadioButton = new System.Windows.Forms.RadioButton();
            this.deptNameGroupBox = new System.Windows.Forms.GroupBox();
            this.deptNameComboBox = new System.Windows.Forms.ComboBox();
            this.deptGroupGroupBox = new System.Windows.Forms.GroupBox();
            this.deptGroupComboBox = new System.Windows.Forms.ComboBox();
            this.shiftGroupBox = new System.Windows.Forms.GroupBox();
            this.shiftComboBox = new System.Windows.Forms.ComboBox();
            this.vacationHoursGroupBox = new System.Windows.Forms.GroupBox();
            this.sickLeaveHoursGroupBox = new System.Windows.Forms.GroupBox();
            this.sickLeaveHoursNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.yearlySalaryGroupBox = new System.Windows.Forms.GroupBox();
            this.yearlySalaryNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hireDateGroupBox = new System.Windows.Forms.GroupBox();
            this.hireDateTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.vacationHoursNumericUpDown)).BeginInit();
            this.firstNameGroupBox.SuspendLayout();
            this.lastNameGroupBox.SuspendLayout();
            this.middleNameGroupBox.SuspendLayout();
            this.jobTitleGroupBox.SuspendLayout();
            this.birthDateGroupBox.SuspendLayout();
            this.maritalStatusGroupBox.SuspendLayout();
            this.genderGroupBox.SuspendLayout();
            this.deptNameGroupBox.SuspendLayout();
            this.deptGroupGroupBox.SuspendLayout();
            this.shiftGroupBox.SuspendLayout();
            this.vacationHoursGroupBox.SuspendLayout();
            this.sickLeaveHoursGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sickLeaveHoursNumericUpDown)).BeginInit();
            this.yearlySalaryGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yearlySalaryNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.hireDateGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(12, 9);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(95, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "IDNUMBERHERE";
            // 
            // vacationHoursNumericUpDown
            // 
            this.vacationHoursNumericUpDown.Location = new System.Drawing.Point(11, 22);
            this.vacationHoursNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.vacationHoursNumericUpDown.Name = "vacationHoursNumericUpDown";
            this.vacationHoursNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.vacationHoursNumericUpDown.TabIndex = 0;
            // 
            // backButton
            // 
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.backButton.Location = new System.Drawing.Point(22, 3);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // submitButton
            // 
            this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.submitButton.Location = new System.Drawing.Point(103, 3);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 0;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // firstNameGroupBox
            // 
            this.firstNameGroupBox.Controls.Add(this.firstNameTextBox);
            this.firstNameGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firstNameGroupBox.Location = new System.Drawing.Point(15, 25);
            this.firstNameGroupBox.Name = "firstNameGroupBox";
            this.firstNameGroupBox.Size = new System.Drawing.Size(143, 64);
            this.firstNameGroupBox.TabIndex = 0;
            this.firstNameGroupBox.TabStop = false;
            this.firstNameGroupBox.Text = "First Name";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(21, 22);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.firstNameTextBox.TabIndex = 0;
            // 
            // lastNameGroupBox
            // 
            this.lastNameGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.lastNameGroupBox.Controls.Add(this.lastNameTextBox);
            this.lastNameGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastNameGroupBox.Location = new System.Drawing.Point(313, 25);
            this.lastNameGroupBox.Name = "lastNameGroupBox";
            this.lastNameGroupBox.Size = new System.Drawing.Size(143, 64);
            this.lastNameGroupBox.TabIndex = 2;
            this.lastNameGroupBox.TabStop = false;
            this.lastNameGroupBox.Text = "Last Name";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(21, 22);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.lastNameTextBox.TabIndex = 0;
            // 
            // middleNameGroupBox
            // 
            this.middleNameGroupBox.Controls.Add(this.middleNameTextBox);
            this.middleNameGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.middleNameGroupBox.Location = new System.Drawing.Point(164, 25);
            this.middleNameGroupBox.Name = "middleNameGroupBox";
            this.middleNameGroupBox.Size = new System.Drawing.Size(143, 64);
            this.middleNameGroupBox.TabIndex = 1;
            this.middleNameGroupBox.TabStop = false;
            this.middleNameGroupBox.Text = "Middle Name";
            // 
            // middleNameTextBox
            // 
            this.middleNameTextBox.Location = new System.Drawing.Point(21, 22);
            this.middleNameTextBox.Name = "middleNameTextBox";
            this.middleNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.middleNameTextBox.TabIndex = 0;
            // 
            // jobTitleGroupBox
            // 
            this.jobTitleGroupBox.Controls.Add(this.jobTitleComboBox);
            this.jobTitleGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.jobTitleGroupBox.Location = new System.Drawing.Point(15, 165);
            this.jobTitleGroupBox.Name = "jobTitleGroupBox";
            this.jobTitleGroupBox.Size = new System.Drawing.Size(143, 64);
            this.jobTitleGroupBox.TabIndex = 6;
            this.jobTitleGroupBox.TabStop = false;
            this.jobTitleGroupBox.Text = "Job Title";
            // 
            // jobTitleComboBox
            // 
            this.jobTitleComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.jobTitleComboBox.FormattingEnabled = true;
            this.jobTitleComboBox.Location = new System.Drawing.Point(11, 22);
            this.jobTitleComboBox.Name = "jobTitleComboBox";
            this.jobTitleComboBox.Size = new System.Drawing.Size(121, 21);
            this.jobTitleComboBox.TabIndex = 0;
            // 
            // birthDateGroupBox
            // 
            this.birthDateGroupBox.Controls.Add(this.birthDateTextBox);
            this.birthDateGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.birthDateGroupBox.Location = new System.Drawing.Point(164, 95);
            this.birthDateGroupBox.Name = "birthDateGroupBox";
            this.birthDateGroupBox.Size = new System.Drawing.Size(143, 64);
            this.birthDateGroupBox.TabIndex = 4;
            this.birthDateGroupBox.TabStop = false;
            this.birthDateGroupBox.Text = "Birth Date";
            // 
            // birthDateTextBox
            // 
            this.birthDateTextBox.Location = new System.Drawing.Point(21, 22);
            this.birthDateTextBox.Name = "birthDateTextBox";
            this.birthDateTextBox.Size = new System.Drawing.Size(100, 20);
            this.birthDateTextBox.TabIndex = 0;
            // 
            // maritalStatusGroupBox
            // 
            this.maritalStatusGroupBox.Controls.Add(this.singleRadioButton);
            this.maritalStatusGroupBox.Controls.Add(this.marriedRadioButton);
            this.maritalStatusGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maritalStatusGroupBox.Location = new System.Drawing.Point(313, 95);
            this.maritalStatusGroupBox.Name = "maritalStatusGroupBox";
            this.maritalStatusGroupBox.Size = new System.Drawing.Size(143, 64);
            this.maritalStatusGroupBox.TabIndex = 5;
            this.maritalStatusGroupBox.TabStop = false;
            this.maritalStatusGroupBox.Text = "Marital Status";
            // 
            // singleRadioButton
            // 
            this.singleRadioButton.AutoSize = true;
            this.singleRadioButton.Location = new System.Drawing.Point(78, 24);
            this.singleRadioButton.Name = "singleRadioButton";
            this.singleRadioButton.Size = new System.Drawing.Size(54, 17);
            this.singleRadioButton.TabIndex = 1;
            this.singleRadioButton.TabStop = true;
            this.singleRadioButton.Text = "Single";
            this.singleRadioButton.UseVisualStyleBackColor = true;
            // 
            // marriedRadioButton
            // 
            this.marriedRadioButton.AutoSize = true;
            this.marriedRadioButton.Location = new System.Drawing.Point(11, 24);
            this.marriedRadioButton.Name = "marriedRadioButton";
            this.marriedRadioButton.Size = new System.Drawing.Size(60, 17);
            this.marriedRadioButton.TabIndex = 0;
            this.marriedRadioButton.TabStop = true;
            this.marriedRadioButton.Text = "Married";
            this.marriedRadioButton.UseVisualStyleBackColor = true;
            // 
            // genderGroupBox
            // 
            this.genderGroupBox.Controls.Add(this.MaleRadioButton);
            this.genderGroupBox.Controls.Add(this.femaleRadioButton);
            this.genderGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.genderGroupBox.Location = new System.Drawing.Point(15, 95);
            this.genderGroupBox.Name = "genderGroupBox";
            this.genderGroupBox.Size = new System.Drawing.Size(143, 64);
            this.genderGroupBox.TabIndex = 3;
            this.genderGroupBox.TabStop = false;
            this.genderGroupBox.Text = "Gender";
            // 
            // MaleRadioButton
            // 
            this.MaleRadioButton.AutoSize = true;
            this.MaleRadioButton.Location = new System.Drawing.Point(78, 24);
            this.MaleRadioButton.Name = "MaleRadioButton";
            this.MaleRadioButton.Size = new System.Drawing.Size(48, 17);
            this.MaleRadioButton.TabIndex = 1;
            this.MaleRadioButton.TabStop = true;
            this.MaleRadioButton.Text = "Male";
            this.MaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // femaleRadioButton
            // 
            this.femaleRadioButton.AutoSize = true;
            this.femaleRadioButton.Location = new System.Drawing.Point(11, 24);
            this.femaleRadioButton.Name = "femaleRadioButton";
            this.femaleRadioButton.Size = new System.Drawing.Size(59, 17);
            this.femaleRadioButton.TabIndex = 0;
            this.femaleRadioButton.TabStop = true;
            this.femaleRadioButton.Text = "Female";
            this.femaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // deptNameGroupBox
            // 
            this.deptNameGroupBox.Controls.Add(this.deptNameComboBox);
            this.deptNameGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deptNameGroupBox.Location = new System.Drawing.Point(164, 165);
            this.deptNameGroupBox.Name = "deptNameGroupBox";
            this.deptNameGroupBox.Size = new System.Drawing.Size(143, 64);
            this.deptNameGroupBox.TabIndex = 7;
            this.deptNameGroupBox.TabStop = false;
            this.deptNameGroupBox.Text = "Department Name";
            // 
            // deptNameComboBox
            // 
            this.deptNameComboBox.FormattingEnabled = true;
            this.deptNameComboBox.Location = new System.Drawing.Point(11, 22);
            this.deptNameComboBox.Name = "deptNameComboBox";
            this.deptNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.deptNameComboBox.TabIndex = 0;
            this.deptNameComboBox.SelectedIndexChanged += new System.EventHandler(this.deptNameComboBox_SelectedIndexChanged);
            // 
            // deptGroupGroupBox
            // 
            this.deptGroupGroupBox.Controls.Add(this.deptGroupComboBox);
            this.deptGroupGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deptGroupGroupBox.Location = new System.Drawing.Point(313, 165);
            this.deptGroupGroupBox.Name = "deptGroupGroupBox";
            this.deptGroupGroupBox.Size = new System.Drawing.Size(143, 64);
            this.deptGroupGroupBox.TabIndex = 8;
            this.deptGroupGroupBox.TabStop = false;
            this.deptGroupGroupBox.Text = "Department Group";
            // 
            // deptGroupComboBox
            // 
            this.deptGroupComboBox.FormattingEnabled = true;
            this.deptGroupComboBox.Location = new System.Drawing.Point(11, 22);
            this.deptGroupComboBox.Name = "deptGroupComboBox";
            this.deptGroupComboBox.Size = new System.Drawing.Size(121, 21);
            this.deptGroupComboBox.TabIndex = 0;
            this.deptGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.deptGroupComboBox_SelectedIndexChanged);
            // 
            // shiftGroupBox
            // 
            this.shiftGroupBox.Controls.Add(this.shiftComboBox);
            this.shiftGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shiftGroupBox.Location = new System.Drawing.Point(15, 235);
            this.shiftGroupBox.Name = "shiftGroupBox";
            this.shiftGroupBox.Size = new System.Drawing.Size(143, 64);
            this.shiftGroupBox.TabIndex = 9;
            this.shiftGroupBox.TabStop = false;
            this.shiftGroupBox.Text = "Shift";
            // 
            // shiftComboBox
            // 
            this.shiftComboBox.FormattingEnabled = true;
            this.shiftComboBox.Location = new System.Drawing.Point(11, 22);
            this.shiftComboBox.Name = "shiftComboBox";
            this.shiftComboBox.Size = new System.Drawing.Size(121, 21);
            this.shiftComboBox.TabIndex = 0;
            // 
            // vacationHoursGroupBox
            // 
            this.vacationHoursGroupBox.Controls.Add(this.vacationHoursNumericUpDown);
            this.vacationHoursGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vacationHoursGroupBox.Location = new System.Drawing.Point(164, 235);
            this.vacationHoursGroupBox.Name = "vacationHoursGroupBox";
            this.vacationHoursGroupBox.Size = new System.Drawing.Size(143, 64);
            this.vacationHoursGroupBox.TabIndex = 10;
            this.vacationHoursGroupBox.TabStop = false;
            this.vacationHoursGroupBox.Text = "Vacation Hours";
            // 
            // sickLeaveHoursGroupBox
            // 
            this.sickLeaveHoursGroupBox.Controls.Add(this.sickLeaveHoursNumericUpDown);
            this.sickLeaveHoursGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sickLeaveHoursGroupBox.Location = new System.Drawing.Point(313, 235);
            this.sickLeaveHoursGroupBox.Name = "sickLeaveHoursGroupBox";
            this.sickLeaveHoursGroupBox.Size = new System.Drawing.Size(143, 64);
            this.sickLeaveHoursGroupBox.TabIndex = 11;
            this.sickLeaveHoursGroupBox.TabStop = false;
            this.sickLeaveHoursGroupBox.Text = "Sick Leave Hours";
            // 
            // sickLeaveHoursNumericUpDown
            // 
            this.sickLeaveHoursNumericUpDown.Location = new System.Drawing.Point(11, 22);
            this.sickLeaveHoursNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sickLeaveHoursNumericUpDown.Name = "sickLeaveHoursNumericUpDown";
            this.sickLeaveHoursNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.sickLeaveHoursNumericUpDown.TabIndex = 0;
            // 
            // yearlySalaryGroupBox
            // 
            this.yearlySalaryGroupBox.Controls.Add(this.yearlySalaryNumericUpDown);
            this.yearlySalaryGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yearlySalaryGroupBox.Location = new System.Drawing.Point(238, 305);
            this.yearlySalaryGroupBox.Name = "yearlySalaryGroupBox";
            this.yearlySalaryGroupBox.Size = new System.Drawing.Size(143, 64);
            this.yearlySalaryGroupBox.TabIndex = 12;
            this.yearlySalaryGroupBox.TabStop = false;
            this.yearlySalaryGroupBox.Text = "Yearly Salary";
            // 
            // yearlySalaryNumericUpDown
            // 
            this.yearlySalaryNumericUpDown.DecimalPlaces = 3;
            this.yearlySalaryNumericUpDown.Location = new System.Drawing.Point(11, 22);
            this.yearlySalaryNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.yearlySalaryNumericUpDown.Name = "yearlySalaryNumericUpDown";
            this.yearlySalaryNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.yearlySalaryNumericUpDown.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.submitButton);
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Location = new System.Drawing.Point(135, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 30);
            this.panel1.TabIndex = 12;
            // 
            // hireDateGroupBox
            // 
            this.hireDateGroupBox.Controls.Add(this.hireDateTextBox);
            this.hireDateGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hireDateGroupBox.Location = new System.Drawing.Point(89, 305);
            this.hireDateGroupBox.Name = "hireDateGroupBox";
            this.hireDateGroupBox.Size = new System.Drawing.Size(143, 64);
            this.hireDateGroupBox.TabIndex = 11;
            this.hireDateGroupBox.TabStop = false;
            this.hireDateGroupBox.Text = "Hire Date";
            // 
            // hireDateTextBox
            // 
            this.hireDateTextBox.Location = new System.Drawing.Point(21, 22);
            this.hireDateTextBox.Name = "hireDateTextBox";
            this.hireDateTextBox.ReadOnly = true;
            this.hireDateTextBox.Size = new System.Drawing.Size(100, 20);
            this.hireDateTextBox.TabIndex = 0;
            // 
            // EditEmployeeInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 411);
            this.Controls.Add(this.hireDateGroupBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.yearlySalaryGroupBox);
            this.Controls.Add(this.sickLeaveHoursGroupBox);
            this.Controls.Add(this.vacationHoursGroupBox);
            this.Controls.Add(this.shiftGroupBox);
            this.Controls.Add(this.deptGroupGroupBox);
            this.Controls.Add(this.deptNameGroupBox);
            this.Controls.Add(this.genderGroupBox);
            this.Controls.Add(this.maritalStatusGroupBox);
            this.Controls.Add(this.birthDateGroupBox);
            this.Controls.Add(this.jobTitleGroupBox);
            this.Controls.Add(this.middleNameGroupBox);
            this.Controls.Add(this.lastNameGroupBox);
            this.Controls.Add(this.firstNameGroupBox);
            this.Controls.Add(this.idLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EditEmployeeInformationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit Employee";
            this.Load += new System.EventHandler(this.InitialLoadForm);
            ((System.ComponentModel.ISupportInitialize)(this.vacationHoursNumericUpDown)).EndInit();
            this.firstNameGroupBox.ResumeLayout(false);
            this.firstNameGroupBox.PerformLayout();
            this.lastNameGroupBox.ResumeLayout(false);
            this.lastNameGroupBox.PerformLayout();
            this.middleNameGroupBox.ResumeLayout(false);
            this.middleNameGroupBox.PerformLayout();
            this.jobTitleGroupBox.ResumeLayout(false);
            this.birthDateGroupBox.ResumeLayout(false);
            this.birthDateGroupBox.PerformLayout();
            this.maritalStatusGroupBox.ResumeLayout(false);
            this.maritalStatusGroupBox.PerformLayout();
            this.genderGroupBox.ResumeLayout(false);
            this.genderGroupBox.PerformLayout();
            this.deptNameGroupBox.ResumeLayout(false);
            this.deptGroupGroupBox.ResumeLayout(false);
            this.shiftGroupBox.ResumeLayout(false);
            this.vacationHoursGroupBox.ResumeLayout(false);
            this.sickLeaveHoursGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sickLeaveHoursNumericUpDown)).EndInit();
            this.yearlySalaryGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.yearlySalaryNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.hireDateGroupBox.ResumeLayout(false);
            this.hireDateGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.NumericUpDown vacationHoursNumericUpDown;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.GroupBox firstNameGroupBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.GroupBox lastNameGroupBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.GroupBox middleNameGroupBox;
        private System.Windows.Forms.TextBox middleNameTextBox;
        private System.Windows.Forms.GroupBox jobTitleGroupBox;
        private System.Windows.Forms.GroupBox birthDateGroupBox;
        private System.Windows.Forms.GroupBox maritalStatusGroupBox;
        private System.Windows.Forms.ComboBox jobTitleComboBox;
        private System.Windows.Forms.RadioButton singleRadioButton;
        private System.Windows.Forms.RadioButton marriedRadioButton;
        private System.Windows.Forms.TextBox birthDateTextBox;
        private System.Windows.Forms.GroupBox genderGroupBox;
        private System.Windows.Forms.RadioButton MaleRadioButton;
        private System.Windows.Forms.RadioButton femaleRadioButton;
        private System.Windows.Forms.GroupBox deptNameGroupBox;
        private System.Windows.Forms.ComboBox deptNameComboBox;
        private System.Windows.Forms.GroupBox deptGroupGroupBox;
        private System.Windows.Forms.ComboBox deptGroupComboBox;
        private System.Windows.Forms.GroupBox shiftGroupBox;
        private System.Windows.Forms.ComboBox shiftComboBox;
        private System.Windows.Forms.GroupBox vacationHoursGroupBox;
        private System.Windows.Forms.GroupBox sickLeaveHoursGroupBox;
        private System.Windows.Forms.NumericUpDown sickLeaveHoursNumericUpDown;
        private System.Windows.Forms.GroupBox yearlySalaryGroupBox;
        private System.Windows.Forms.NumericUpDown yearlySalaryNumericUpDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox hireDateGroupBox;
        private System.Windows.Forms.TextBox hireDateTextBox;
    }
}