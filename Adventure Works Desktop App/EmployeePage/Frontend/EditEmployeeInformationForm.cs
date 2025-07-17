using Adventure_Works_Desktop_App.Globals.DataClasses;
using Adventure_Works_Desktop_App.EmployeePage.Backend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Frontend
{
    public partial class EditEmployeeInformationForm : Form
    {
        public bool submitButtonPressed = false;
        public EmployeeData data;
        EditEmployeeBackend backend;
        public EditEmployeeInformationForm(EmployeeData data)
        {
            InitializeComponent();
            this.data = data;
            backend = new EditEmployeeBackend(idLabel.Text);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            submitButtonPressed = true;

            if (ValidComboBoxes(deptGroupComboBox, "Please enter a valid Department Group"))
            {
                return;
            }

            if (ValidComboBoxes(deptNameComboBox, "Please enter a valid Department Name"))
            {
                return;
            }

            if (ValidComboBoxes(jobTitleComboBox, "Please enter a valid Job Title"))
            {
                return;
            }

            if (ValidComboBoxes(shiftComboBox, "Please enter a valid Shift"))
            {
                return;
            }

            this.data = PackageData();
        }

        private bool ValidComboBoxes(ComboBox combobox, string message)
        {
            if (!combobox.Items.Contains(combobox.Text))
            {
                combobox.Text = "Select New Item";
                MessageBox.Show(message);
                return true;
            }
            return false;
        }

        private void InitialLoadForm(object sender, EventArgs e)
        {
            LoadEmployeeData();
            AddItemsToComboBoxes();
        }

        private void LoadEmployeeData()
        {
            idLabel.Text = data.BusinessEntityID;
            firstNameTextBox.Text = data.FirstName;
            middleNameTextBox.Text = data.MiddleName;
            lastNameTextBox.Text = data.LastName;

            if (data.Gender == "F")
            {
                femaleRadioButton.Checked = true;
            }
            else
            {
                MaleRadioButton.Checked = true;
            }

            birthDateTextBox.Text = data.BirthDate;

            if (data.MaritalStatus == "M")
            {
                marriedRadioButton.Checked = true;
            }
            else
            {
                singleRadioButton.Checked = true;
            }

            jobTitleComboBox.Text = data.JobTitle;

            deptNameComboBox.Text = data.DepartmentName;

            deptGroupComboBox.Text = data.DepartmentGroupName;

            shiftComboBox.Text = data.ShiftName;

            vacationHoursNumericUpDown.Value = Convert.ToInt32(data.VacationHours);

            sickLeaveHoursNumericUpDown.Value = Convert.ToInt32(data.SickLeaveHours);

            hireDateTextBox.Text = data.HireDate;

            yearlySalaryNumericUpDown.Value = decimal.Parse(data.YearlySalary);
        }

        private EmployeeData PackageData()
        {
            string married = "";
            string gender = "";
            
            if (marriedRadioButton.Checked)
            {
                married = "M";
            }
            else
            {
                married = "S";
            }

            if (femaleRadioButton.Checked)
            {
                gender = "F";
            }
            else
            {
                gender = "M";
            }


            EmployeeData temp = new EmployeeData(
                idLabel.Text, firstNameTextBox.Text, middleNameTextBox.Text, lastNameTextBox.Text,
                jobTitleComboBox.Text, birthDateTextBox.Text, married, gender, hireDateTextBox.Text, $"{vacationHoursNumericUpDown.Value}",
                $"{sickLeaveHoursNumericUpDown.Value}", deptNameComboBox.Text, deptGroupComboBox.Text, shiftComboBox.Text,
                $"{yearlySalaryNumericUpDown.Value}"
                );

            // update db with new employee data
            backend.PushToSql( temp );
            
            return temp;
        }

        private void AddItemsToComboBoxes()
        {
            // jobtitle, deptname, deptgroup, shift
            AddItemsJobTitle();
            AddItemsDeptName();
            AddItemsDeptGroup();
            AddItemsShift();
        }

        private void AddItemsJobTitle()
        {
            backend.FillItems(jobTitleComboBox, "Job", deptNameComboBox.Text);
        }

        private void AddItemsDeptName()
        { 
            backend.FillItems(deptNameComboBox, "Name", deptGroupComboBox.Text);
        }

        private void AddItemsDeptGroup()
        {
            backend.FillItems(deptGroupComboBox, "Group", "");
        }

        private void AddItemsShift()
        {
            backend.FillItems(shiftComboBox, "Shift", "");
        }

        private void deptGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddItemsDeptName();
            deptNameComboBox.SelectedIndex = 0;
        }

        private void deptNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            AddItemsJobTitle();
            jobTitleComboBox.SelectedIndex = 0;
        }
    }
}
