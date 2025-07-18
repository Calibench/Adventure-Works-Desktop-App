using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    /// <summary>
    /// Handles the backend of the edit employee page
    /// </summary>
    internal class EditEmployeeBackend
    {
        private Connection connect = new Connection();
        
        /// <summary>
        /// Fills the items in the job title combobox on the front end.
        /// </summary>
        /// <param name="combobox">JobTitleComboBox should be parsed here</param>
        /// <param name="deptName">The name of the department</param>
        public void FillItemsJobTitle(ComboBox combobox, string deptName)
        {
            GenStoredProc(combobox, "dbo.uspGetAllUniqueJobTitles", "@DeptName", deptName, "JobTitle");
        }

        /// <summary>
        /// Fills the items in the department name combobox on the front end. 
        /// </summary>
        /// <param name="combobox">DepartmentNameComboBox should be parsed here</param>
        /// <param name="groupName">The name of the department group</param>
        public void FillItemsDepartmentName(ComboBox combobox, string groupName)
        {
            GenStoredProc(combobox, "dbo.uspGetDepartmentName", "@GroupName", groupName, "name");
        }

        /// <summary>
        /// Fills the items in the department group name combobox on the front end.
        /// </summary>
        /// <param name="combobox">DepartmentGroupNameComboBox should be parsed here</param>
        public void FillItemsGroup(ComboBox combobox)
        {
            GenStoredProc(combobox, "dbo.uspGetUniqueDepartmentGroupNames", null, null, "GroupName");
        }

        /// <summary>
        /// Fills the items in the shift name combobox on the front end.
        /// </summary>
        /// <param name="combobox">ShiftNameComboBox should be parsed here</param>
        public void FillItemsShift(ComboBox combobox)
        {
            GenStoredProc(combobox, "uspGetAllShiftNames", null, null, "Name");
        }

        /// <summary>
        /// Runs a generalized stored proc that takes in one param. Used to get a list of data into a ComboBox.
        /// </summary>
        /// <param name="combobox">Combobox that needs to be filled with items</param>
        /// <param name="query">Query to run and access the DB with</param>
        /// <param name="param">The parameter that is needed to gain specific data from the DB</param>
        /// <param name="name">The value needed to be replace the parameter</param>
        /// <param name="columnHeader">The header of the column needed</param>
        /// <exception cref="Exception">Connection to database is inaccessible</exception>
        private void GenStoredProc(ComboBox combobox, string query, string param, string name, string columnHeader)
        {
            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null && name != null)
                    {
                        cmd.Parameters.AddWithValue(param, name);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        combobox.Items.Clear();
                        while (reader.Read())
                        {
                            combobox.Items.Add(reader[columnHeader].ToString());
                        }
                        return;
                    }
                }
            }
            throw new Exception("Error with Fetching Item " + query);
        }

        /// <summary>
        /// Wrapper to push employee data to the DB
        /// </summary>
        /// <param name="data">The employee data that is being pushed</param>
        public void PushToSql(EmployeeData data)
        {
            UpdateBasicEmployeeInfo(data);
            UpdateBasicPersonInfo(data);
            UpdateEmployeeDepartmentHistory(data);
            UpdateEmployeePayHistory(data);
        }

        /// <summary>
        /// Updates the HumanResources.Employee's table to the new general employee data
        /// </summary>
        /// <param name="data">The data being used to update the employee</param>
        /// <exception cref="Exception">Cannot connect to the DB</exception>
        private void UpdateBasicEmployeeInfo(EmployeeData data)
        {
            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.uspUpdateEmployeeData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessEntityID", data.BusinessEntityID);
                    cmd.Parameters.AddWithValue("@JobTitle", data.JobTitle);
                    cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                    cmd.Parameters.AddWithValue("@MaritalStatus", data.MaritalStatus);
                    cmd.Parameters.AddWithValue("@Gender", data.Gender);
                    cmd.Parameters.AddWithValue("@HireDate", data.HireDate);
                    cmd.Parameters.AddWithValue("@VacationHours", data.VacationHours);
                    cmd.Parameters.AddWithValue("@SickLeaveHours", data.SickLeaveHours);
                    cmd.ExecuteNonQuery();
                    return;
                }
            }
            throw new Exception("--Cannot Update Employee Info--");
        }

        /// <summary>
        /// Updates the Person.Person table to the new employee's full name
        /// </summary>
        /// <param name="data">The data being used to update the employee</param>
        /// <exception cref="Exception">Cannot connect to the DB</exception>
        private void UpdateBasicPersonInfo(EmployeeData data)
        {
            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.uspUpdatePersonEmployeeName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", data.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@BusinessEntityID", data.BusinessEntityID);
                    cmd.ExecuteNonQuery();
                    return;
                }
            }
            throw new Exception("--Cannot Update Employee Name Info--");
        }

        /// <summary>
        /// Updates the HumanResources.EmployeeDepartmentHistory with the new department of the employee
        /// </summary>
        /// <param name="data">The data being used to update the employee</param>
        /// <exception cref="Exception">Cannot connect to the DB</exception>
        private void UpdateEmployeeDepartmentHistory(EmployeeData data)
        {
            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.uspUpdateEmployeeDepartmentHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentID", FindDepartmentDetail(data));
                    cmd.Parameters.AddWithValue("@ShiftID", FindShiftDetails(data));
                    cmd.Parameters.AddWithValue("@BusinessEntityID", data.BusinessEntityID);
                    cmd.ExecuteNonQuery();
                    return;
                }
            }
            throw new Exception("--Cannot Update Employee Department History--");
        }

        /// <summary>
        /// Inserts into HumanResources.EmployeePayHistory the new employee pay
        /// </summary>
        /// <param name="data">The data being used to update the employee</param>
        /// <exception cref="Exception"/>Cannot connect to the DB</exception>
        private void UpdateEmployeePayHistory(EmployeeData data)
        {
            // Convert yearly to hourly pay
            decimal reverseSalaryToRate = decimal.Parse(data.YearlySalary);
            reverseSalaryToRate /= 52; // 52 weeks -- now weekly pay
            reverseSalaryToRate /= 40; // 40 hours -- now hourly pay
            string salaryRate = reverseSalaryToRate.ToString();

            // if there has been no change to pay then don't bother inserting new pay history
            if (!CheckSalaryChange(salaryRate, data))
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.uspInsertEmployeeNewPay", conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessEntityID", data.BusinessEntityID);
                    cmd.Parameters.AddWithValue("@RateChangeDate", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@NewRate", salaryRate);
                    cmd.Parameters.AddWithValue("@PayFrequency", GetPayFreq(data));
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();
                    return;
                }
            }
            throw new Exception("--Cannot Insert New Pay For Employee--");
        }

        /// <summary>
        /// Compares the salary of the one being displayed on the front end and the one in the database.
        /// </summary>
        /// <param name="salaryRate">The salary that is being displayed on the front end</param>
        /// <param name="data">Employee data - Used to get the ID of the employee</param>
        /// <returns>A <see cref="bool"> is returned, True if the salary is different to the one on the backend, else False</returns>
        /// <exception cref="Exception">Employee cannot be found | DB Error</exception>
        private bool CheckSalaryChange(string salaryRate, EmployeeData data)
        {
            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select dbo.ufnGetEmployeePayRate(@BusinessEntityID)", conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessEntityID", data.BusinessEntityID);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        if (result.ToString().Equals(salaryRate))
                        {
                            return false;
                        }
                        return true;
                    }
                    throw new Exception("--Cannot Find Employee In The System--");
                }
            }
            throw new Exception("--Cannot Check Employee Salary--");
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

            return GenFetchOneItem(query, param, id);
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

            return GenFetchOneItem(query, param, name);
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

            return GenFetchOneItem(query, param, name);
        }

        /// <summary>
        /// The generalization of grabbing one cell from the database
        /// </summary>
        /// <param name="query">A Scalar Function</param>
        /// <param name="param">The parameter of the scalar function</param>
        /// <param name="data">Data to set the parameter to</param>
        /// <returns>Returns the string of the cell that was found</returns>
        /// <exception cref="ArgumentException">Unable to access DB</exception>
        private string GenFetchOneItem(string query, string param, string data)
        {
            using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(param, data);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            throw new ArgumentException("Error with Fetching Item", nameof(query));
        }
    }
}
