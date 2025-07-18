using Adventure_Works_Desktop_App.EmployeePage.Backend;
using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Frontend
{
    public partial class EditEmployeeInformationForm : Form
    {
        private EmployeeData eData;
        private EditEmployeeBackend backend;
        private bool initial;

        public EditEmployeeInformationForm(EmployeeData eData)
        {
            InitializeComponent();
            this.eData = eData;
            backend = new EditEmployeeBackend();
        }

        // Event Driven Methods
        private void InitialLoadForm(object sender, EventArgs e)
        {
            initial = true;
            LoadEmployeeData();
            AddItemsToComboBoxes();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            PackageData();
        }

        private void deptGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Will do nothing upon first load
            if (this.initial)
            {
                return;
            }
            AddItemsDeptName();
        }

        private void deptNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Will do nothing upon first load
            if (this.initial)
            {
                return;
            }
            AddItemsJobTitle();
        }

        // Helper Methods
        /// <summary>
        /// Loads data from eData to the necessary fields in the frontend
        /// </summary>
        private void LoadEmployeeData()
        {
            idLabel.Text = eData.BusinessEntityID;
            firstNameTextBox.Text = eData.FirstName;
            middleNameTextBox.Text = eData.MiddleName;
            lastNameTextBox.Text = eData.LastName;

            if (eData.Gender == "F")
            {
                femaleRadioButton.Checked = true;
            }
            else
            {
                MaleRadioButton.Checked = true;
            }

            birthDateTextBox.Text = eData.BirthDate;

            if (eData.MaritalStatus == "M")
            {
                marriedRadioButton.Checked = true;
            }
            else
            {
                singleRadioButton.Checked = true;
            }

            jobTitleComboBox.Text = eData.JobTitle;
            deptNameComboBox.Text = eData.DepartmentName;
            deptGroupComboBox.Text = eData.DepartmentGroupName;
            shiftComboBox.Text = eData.ShiftName;
            vacationHoursNumericUpDown.Value = Convert.ToInt32(eData.VacationHours);
            sickLeaveHoursNumericUpDown.Value = Convert.ToInt32(eData.SickLeaveHours);
            hireDateTextBox.Text = eData.HireDate;
            yearlySalaryNumericUpDown.Value = decimal.Parse(eData.YearlySalary);
        }

        /// <summary>
        /// Packages the data from the frontend gui to send to the backend for processing to DB.
        /// </summary>
        /// <returns>Returns an employee data object to store globally.</returns>
        private void PackageData()
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


            EmployeeData packagedData = new EmployeeData(
                idLabel.Text, firstNameTextBox.Text, middleNameTextBox.Text, lastNameTextBox.Text,
                jobTitleComboBox.Text, birthDateTextBox.Text, married, gender, hireDateTextBox.Text, vacationHoursNumericUpDown.Value.ToString(),
                sickLeaveHoursNumericUpDown.Value.ToString(), deptNameComboBox.Text, deptGroupComboBox.Text, shiftComboBox.Text,
                yearlySalaryNumericUpDown.Value.ToString()
                );

            // update db with new employee data
            backend.PushToSql(packagedData);
        }

        /// <summary>
        /// Initial ComboBox Updater. Wraps the AddItems* methods.
        /// </summary>
        private void AddItemsToComboBoxes()
        {
            AddItemsDeptGroup();
            AddItemsDeptName();
            AddItemsJobTitle();
            AddItemsShift();

            this.initial = false;
        }

        /// <summary>
        /// Calls the backend to fill the job titles combobox based off of the department that is currently selected
        /// </summary>
        private void AddItemsJobTitle()
        {
            backend.FillItemsJobTitle(jobTitleComboBox, deptNameComboBox.Text);
            SelectInitialItem(jobTitleComboBox, eData.JobTitle);
        }

        /// <summary>
        /// Calls the backend to fill the department names combobox based off of the group that is currently selected
        /// </summary>
        private void AddItemsDeptName()
        { 
            backend.FillItemsDepartmentName(deptNameComboBox, deptGroupComboBox.Text);
            SelectInitialItem(deptNameComboBox, eData.DepartmentName);
        }

        /// <summary>
        /// Calls the backend to fill the department group combobox with the group names from the database
        /// </summary>
        private void AddItemsDeptGroup()
        {
            backend.FillItemsGroup(deptGroupComboBox);
            SelectInitialItem(deptGroupComboBox, eData.DepartmentGroupName);
        }

        /// <summary>
        /// Calls the backend to fill the shift combobox with the shift names from the database
        /// </summary>
        private void AddItemsShift()
        {
            backend.FillItemsShift(shiftComboBox);
            SelectInitialItem(shiftComboBox, eData.ShiftName);
        }

        /// <summary>
        /// Selects the index of the one that matches the eData. Additionally handles if it is the initial startup of the form's comboboxes.
        /// </summary>
        /// <param name="comboBox">combobox to sort through and check the items</param>
        /// <param name="data">data that is from eData.</param>
        private void SelectInitialItem(ComboBox comboBox, string data)
        {
            if (initial)
            {
                int index = 0;
                foreach (string item in comboBox.Items)
                {
                    if (item.Equals(data))
                    {
                        comboBox.SelectedIndex = index;
                    }
                    index++;
                }
            }
            else
            {
                comboBox.SelectedIndex = 0;
            }
        }
    }
}
