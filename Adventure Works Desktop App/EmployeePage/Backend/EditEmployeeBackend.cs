using Adventure_Works_Desktop_App.Globals.DataClasses;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    /// <summary>
    /// Handles the backend of the edit employee page
    /// </summary>
    internal class EditEmployeeBackend
    {
        /// <summary>
        /// Fills the items in the job title combobox on the front end.
        /// </summary>
        /// <param name="combobox">JobTitleComboBox should be parsed here</param>
        /// <param name="deptName">The name of the department</param>
        public void FillItemsJobTitle(ComboBox combobox, string deptName)
        {
            string query = "dbo.uspGetAllUniqueJobTitles",
                   columnHeader = "JobTitle",
                   param = "@DeptName",
                   value = deptName;
            EmployeeDAL.DBGenComboBoxStoredProc(combobox, query, columnHeader, param, value);
        }

        /// <summary>
        /// Fills the items in the department name combobox on the front end. 
        /// </summary>
        /// <param name="combobox">DepartmentNameComboBox should be parsed here</param>
        /// <param name="groupName">The name of the department group</param>
        public void FillItemsDepartmentName(ComboBox combobox, string groupName)
        {
            string query = "dbo.uspGetDepartmentName",
                   columnHeader = "Name",
                   param = "@GroupName",
                   value = groupName;
            EmployeeDAL.DBGenComboBoxStoredProc(combobox, query, columnHeader, param, value);
        }

        /// <summary>
        /// Fills the items in the department group name combobox on the front end.
        /// </summary>
        /// <param name="combobox">DepartmentGroupNameComboBox should be parsed here</param>
        public void FillItemsGroup(ComboBox combobox)
        {
            string query = "dbo.uspGetUniqueDepartmentGroupNames",
                   columnHeader = "GroupName";
            EmployeeDAL.DBGenComboBoxStoredProc(combobox, query, columnHeader, null, null);
        }

        /// <summary>
        /// Fills the items in the shift name combobox on the front end.
        /// </summary>
        /// <param name="combobox">ShiftNameComboBox should be parsed here</param>
        public void FillItemsShift(ComboBox combobox)
        {
            string query = "dbo.uspGetAllShiftNames",
                   columnHeader = "Name";
            EmployeeDAL.DBGenComboBoxStoredProc(combobox, query, columnHeader, null, null);
        }

        /// <summary>
        /// Wrapper to push employee data to the DB
        /// </summary>
        /// <param name="data">The employee data that is being pushed</param>
        public void PushToSql(EmployeeData data)
        {
            EmployeeDAL.DBUBasicEmployeeInfo(data);
            EmployeeDAL.DBUBasicPersonInfo(data);
            EmployeeDAL.DBUEmployeeDepartmentHistory(data, FindDepartmentDetail(data), FindShiftDetails(data));

            // if there has been no change to pay then don't bother inserting new pay history
            if (!EmployeeDAL.DBCheckSalaryChange(SalaryToRate(data.YearlySalary), data))
            {
                return;
            }

            EmployeeDAL.DBIEmployeePayHistory(data, SalaryToRate(data.YearlySalary), GetPayFreq(data));
        }

        /// <summary>
        /// Gets the current pay frequency of the employee
        /// </summary>
        /// <param name="data">Currently selected employee data</param>
        /// <returns>The <see cref="string"/> of the pay rate of the employee</returns>
        private string GetPayFreq(EmployeeData data)
        {
            string query = "select dbo.ufnGetEmployeePayFrequency(@BusinessEntityID)",
                   param = "@BusinessEntityID",
                   id = data.BusinessEntityID;

            return EmployeeDAL.DBGenScalarFuncFetch(query, param, id);
        }

        /// <summary>
        /// Gets the Department ID.
        /// </summary>
        /// <param name="data">Currently selected employee data</param>
        /// <returns>The <see cref="string"/> of Department ID</returns>
        private string FindDepartmentDetail(EmployeeData data)
        {
            string query = "select dbo.ufnGetDepartmentID(@Name)",
                   param = "@Name",
                   name = data.DepartmentName;

            return EmployeeDAL.DBGenScalarFuncFetch(query, param, name);
        }

        /// <summary>
        /// Gets the Shift ID
        /// </summary>
        /// <param name="data">Currently selected employee data</param>
        /// <returns>The <see cref="string"/> of Shift ID</returns>
        private string FindShiftDetails(EmployeeData data)
        {
            string query = "select dbo.ufnGetShiftID(@Name)",
                   param = "@Name",
                   name = data.ShiftName;

            return EmployeeDAL.DBGenScalarFuncFetch(query, param, name);
        }

        #region Helper Methods
        /// <summary>
        /// Converts a yearly salary to an hourly rate.
        /// </summary>
        /// <param name="yearlySalary">The yearly salary as a string. Must be a valid decimal number.</param>
        /// <returns>A string representing the equivalent hourly rate, calculated based on a 52-week year and a 40-hour work
        /// week.</returns>
        private string SalaryToRate(string yearlySalary)
        {
            decimal reverseSalaryToRate = decimal.Parse(yearlySalary);
            reverseSalaryToRate /= 52; // 52 weeks -- now weekly pay
            reverseSalaryToRate /= 40; // 40 hours -- now hourly pay
            return reverseSalaryToRate.ToString();
        }
        #endregion
    }
}