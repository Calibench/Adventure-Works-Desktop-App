using Adventure_Works_Desktop_App.EmployeePage.Frontend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App
{
    public partial class EmployeeInfoForm : Form
    {
        public bool backButtonPressed = false;
        private string username;
        public EmployeeInfoForm(String username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeInfoSearch backend = new EmployeeInfoSearch(searchComboBox, true);
            UpdateFrontEnd(backend.GetEmployeeData());
        }

        private void PopulateComboBox()
        {
            EmployeeInfoSearch combobox = new EmployeeInfoSearch(searchComboBox, false);
        }

        private void UpdateFrontEnd(EmployeeDetails emp)
        {
            businessEntityIDLabel.Text = $"ID: {emp.GetBusinessEntityID()}";
            firstNameLabel.Text = $"First Name: {emp.GetFirstName()}";
            middleInitialLabel.Text = $"Middle Initial: {emp.GetMiddleName()}";
            lastNameLabel.Text = $"Last Name: {emp.GetLastName()}";
            jobTitleLabel.Text = $"Job Title: {emp.GetJobTitle()}";
            birthDateLabel.Text = $"Birth Date: {emp.GetBirthDate()}";
            maritalStatusLabel.Text = $"Marital Status: {emp.GetMaritalStatus()}";
            genderLabel.Text = $"Gender: {emp.GetGender()}";
            hireDateLabel.Text = $"Hire Date: {emp.GetHireDate()}";
            vacationHoursLabel.Text = $"Vacation Hours: {emp.GetVacationHours()}";
            sickLeaveHoursLabel.Text = $"Sick Leave Hours: {emp.GetSickLeaveHours()}";
            departmentNameLabel.Text = $"Department Name: {emp.GetDepartmentName()}";
            departmentGroupLabel.Text = $"Department Group: {emp.GetDepartmentGroupName()}";
            shiftLabel.Text = $"Shift: {emp.GetShiftName()}";
            yearlySalaryLabel.Text = $"Yearly Salary: {emp.GetYearlySalary()}";
            editButton.Visible = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            backButtonPressed = true;
            this.Close();
        }

        private void InitialLoadForm(object sender, EventArgs e)
        {
            usernameLabel.Text = $"Logged in: {username}";
            editButton.Visible = false;
            PopulateComboBox();
        }

        private void EditEmployeeInformation(object sender, EventArgs e)
        {
            EmployeeInfoSearch backend = new EmployeeInfoSearch(searchComboBox, true);
            EmployeeDetails data = backend.GetEmployeeData();

            var frm = new EditEmployeeInformationForm(data);
            frm.Location = this.Location;
            this.Hide();
            frm.ShowDialog();
            if (frm.submitButtonPressed)
            {
                UpdateFrontEnd(frm.data);
            }
            this.Show();
        }
    }
}
