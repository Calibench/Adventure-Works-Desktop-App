using Adventure_Works_Desktop_App.Globals.DataClasses;
using Adventure_Works_Desktop_App.EmployeePage.Backend;
using System;
using System.Windows.Forms;
using Adventure_Works_Desktop_App.Globals;

namespace Adventure_Works_Desktop_App.EmployeePage.Frontend
{
    public partial class EmployeeInfoForm : Form
    {
        // Global Variables
        private string displayName;
        private EmployeeInfoBackend backend = new EmployeeInfoBackend();

        /// <summary>
        /// Constructor to initialize the form and to get the username to display
        /// </summary>
        /// <param name="displayName">The display name that was gotten through the login form</param>
        public EmployeeInfoForm(string displayName)
        {
            InitializeComponent();
            this.displayName = displayName;
        }

        // Event Driven Methods
        private void InitialLoadForm(object sender, EventArgs e)
        {
            editButton.Visible = false;

            // Populates the ComboBox with ID's
            backend = new EmployeeInfoBackend();
            backend.UpdateComoboBox(searchComboBox);
            loggedBackUserControl.ChangeDisplayName(displayName);
        }

        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeData eData = backend.GetData(searchComboBox.Text);
            UpdateFrontEnd(eData);
        }

        private void EditEmployeeInformation(object sender, EventArgs e)
        {
            EmployeeData eData = backend.GetData(searchComboBox.Text);

            EditEmployeeInformationForm editEmployeeForm = new EditEmployeeInformationForm(eData);
            FormNavigationHelper.ShowFormAndHideCurrent(this, editEmployeeForm);

            searchComboBox_SelectedIndexChanged(sender, e);
        }

        // Helper Methods
        /// <summary>
        /// Updates labels to show the selected or updated data
        /// </summary>
        /// <param name="emp">Employee data to use to update the labels</param>
        private void UpdateFrontEnd(EmployeeData emp)
        {
            businessEntityIDLabel.Text = $"ID: {emp.BusinessEntityID}";
            firstNameLabel.Text = $"First Name: {emp.FirstName}";
            middleInitialLabel.Text = $"Middle Initial: {emp.MiddleName}";
            lastNameLabel.Text = $"Last Name: {emp.LastName}";
            jobTitleLabel.Text = $"Job Title: {emp.JobTitle}";
            birthDateLabel.Text = $"Birth Date: {emp.BirthDate}";
            maritalStatusLabel.Text = $"Marital Status: {emp.MaritalStatus}";
            genderLabel.Text = $"Gender: {emp.Gender}";
            hireDateLabel.Text = $"Hire Date: {emp.HireDate}";
            vacationHoursLabel.Text = $"Vacation Hours: {emp.VacationHours}";
            sickLeaveHoursLabel.Text = $"Sick Leave Hours: {emp.SickLeaveHours}";
            departmentNameLabel.Text = $"Department Name: {emp.DepartmentName}";
            departmentGroupLabel.Text = $"Department Group: {emp.DepartmentGroupName}";
            shiftLabel.Text = $"Shift: {emp.ShiftName}";
            yearlySalaryLabel.Text = $"Yearly Salary: {emp.YearlySalary}";
            editButton.Visible = true;
        }
    }
}
