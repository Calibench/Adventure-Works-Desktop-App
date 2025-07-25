using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    internal class EmployeeDAL
    {
        #region Employee Info Backend Data Access Layer
        // Stored Procedures:
        /// <summary>
        /// Retrieves employee data from the database for a specified business entity ID.
        /// </summary>
        /// <remarks>This method executes a stored procedure to fetch employee details from the
        /// AdventureWorks database. Ensure that the connection string for the database is correctly configured in the
        /// application settings.</remarks>
        /// <param name="businessEntityID">The unique identifier for the business entity whose employee data is to be retrieved. Cannot be null or
        /// empty.</param>
        /// <returns>An <see cref="EmployeeData"/> object containing the employee's details if found; otherwise, <see
        /// langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public EmployeeData DBGetEmployeeData(string businessEntityID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetEmployeeData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BusinessEntityID", businessEntityID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new EmployeeData
                                    (
                                     reader["BusinessEntityID"].ToString(),
                                     reader["FirstName"].ToString(),
                                     reader["MiddleName"].ToString(),
                                     reader["LastName"].ToString(),
                                     reader["JobTitle"].ToString(),
                                     reader["BirthDate"].ToString(),
                                     reader["MaritalStatus"].ToString(),
                                     reader["Gender"].ToString(),
                                     reader["HireDate"].ToString(),
                                     reader["VacationHours"].ToString(),
                                     reader["SickLeaveHours"].ToString(),
                                     reader["dep_name"].ToString(),
                                     reader["dep_groupname"].ToString(),
                                     reader["shift_name"].ToString(),
                                     reader["Yearly_Salary"].ToString()
                                    );
                            }
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBGetEmployeeData.", ex);
            }
        }
        #endregion

        #region Edit Employee Backend Data Access Layer
        // Scalar Functions:
        /// <summary>
        /// Executes a scalar SQL query and returns the result as a string.
        /// </summary>
        /// <remarks>This method opens a connection to the AdventureWorks database, executes the provided
        /// scalar query, and returns the result. Ensure that the query and parameters are correctly specified to avoid
        /// SQL exceptions.</remarks>
        /// <param name="query">The SQL query to execute. Must be a valid scalar query.</param>
        /// <param name="param">The name of the parameter to add to the SQL command.</param>
        /// <param name="data">The value to assign to the parameter specified by <paramref name="param"/>.</param>
        /// <returns>The result of the scalar query as a string, or <see langword="null"/> if the query returns no result.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is an error accessing the database.</exception>
        public string DBGenScalarFuncFetch(string query, string param, string data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
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
                return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBGenScalarFuncFetch.", ex);
            }
        }

        /// <summary>
        /// Determines whether the salary rate of the specified employee has changed.
        /// </summary>
        /// <remarks>This method connects to the AdventureWorks database to retrieve the stored salary
        /// rate for the employee identified by the <paramref name="data"/> parameter. It compares the stored rate with
        /// the provided <paramref name="salaryRate"/> to determine if a change has occurred.</remarks>
        /// <param name="salaryRate">The current salary rate to compare against the stored rate.</param>
        /// <param name="data">The employee data containing the business entity ID.</param>
        /// <returns><see langword="true"/> if the salary rate has changed; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the employee cannot be found in the system or if database access fails.</exception>
        public bool DBCheckSalaryChange(string salaryRate, EmployeeData data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
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
                        throw new InvalidOperationException("Cannot find employee in the system.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in CheckSalaryChange.", ex);
            }
        }

        // Stored Procedures:
        /// <summary>
        /// Updates the basic employee information in the database using the specified data.
        /// </summary>
        /// <remarks>This method executes a stored procedure to update employee details such as job title,
        /// birth date, marital status, gender, hire date, vacation hours, and sick leave hours. Ensure that the
        /// <paramref name="eData"/> parameter is not null and contains valid information for each field.</remarks>
        /// <param name="eData">An <see cref="EmployeeData"/> object containing the employee's updated information.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public void DBUBasicEmployeeInfo(EmployeeData eData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspUpdateEmployeeData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BusinessEntityID", eData.BusinessEntityID);
                        cmd.Parameters.AddWithValue("@JobTitle", eData.JobTitle);
                        cmd.Parameters.AddWithValue("@BirthDate", eData.BirthDate);
                        cmd.Parameters.AddWithValue("@MaritalStatus", eData.MaritalStatus);
                        cmd.Parameters.AddWithValue("@Gender", eData.Gender);
                        cmd.Parameters.AddWithValue("@HireDate", eData.HireDate);
                        cmd.Parameters.AddWithValue("@VacationHours", eData.VacationHours);
                        cmd.Parameters.AddWithValue("@SickLeaveHours", eData.SickLeaveHours);
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBUBasicEmployeeInfo.", ex);
            }
        }

        /// <summary>
        /// Updates the basic personal information of an employee in the database.
        /// </summary>
        /// <remarks>This method executes a stored procedure to update the employee's name details in the
        /// AdventureWorks database. Ensure that the provided <paramref name="eData"/> contains valid and complete
        /// information before calling this method.</remarks>
        /// <param name="eData">The employee data containing the first name, middle name, last name, and business entity ID to update.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public void DBUBasicPersonInfo(EmployeeData eData)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspUpdatePersonEmployeeName", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", eData.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", eData.MiddleName);
                        cmd.Parameters.AddWithValue("@LastName", eData.LastName);
                        cmd.Parameters.AddWithValue("@BusinessEntityID", eData.BusinessEntityID);
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBUBasicPersonInfo.", ex);
            }
        }

        /// <summary>
        /// Updates the department and shift information for a specified employee in the database.
        /// </summary>
        /// <remarks>This method updates the employee's department and shift by executing a stored
        /// procedure in the database. Ensure that the provided department and shift identifiers are valid and
        /// correspond to existing records.</remarks>
        /// <param name="eData">The employee data containing the business entity ID of the employee to update.</param>
        /// <param name="department">The identifier of the department to assign to the employee. Cannot be null or empty.</param>
        /// <param name="shift">The identifier of the shift to assign to the employee. Cannot be null or empty.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database during the update operation.</exception>
        public void DBUEmployeeDepartmentHistory(EmployeeData eData, string department, string shift)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspUpdateEmployeeDepartmentHistory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DepartmentID", department);
                        cmd.Parameters.AddWithValue("@ShiftID", shift);
                        cmd.Parameters.AddWithValue("@BusinessEntityID", eData.BusinessEntityID);
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in UpdateEmployeeDepartmentHistory.", ex);
            }
        }

        /// <summary>
        /// Inserts a new pay record for an employee into the database.
        /// </summary>
        /// <remarks>This method uses a stored procedure to insert a new pay record for the specified
        /// employee. The <paramref name="newRate"/> and <paramref name="payFrequency"/> parameters must be valid
        /// strings representing the new pay rate and frequency, respectively.</remarks>
        /// <param name="data">The employee data containing the business entity ID.</param>
        /// <param name="newRate">The new pay rate to be applied to the employee.</param>
        /// <param name="payFrequency">The frequency of the pay period, such as weekly or bi-weekly.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public void DBIEmployeePayHistory(EmployeeData data, string newRate, string payFrequency)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspInsertEmployeeNewPay", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BusinessEntityID", data.BusinessEntityID);
                        cmd.Parameters.AddWithValue("@RateChangeDate", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@NewRate", newRate);
                        cmd.Parameters.AddWithValue("@PayFrequency", payFrequency);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now.ToString());
                        cmd.ExecuteNonQuery();
                        return;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetSalesPersonData.", ex);
            }
        }
        #endregion

        #region Global Employee Backend Data Access Layer
        // Stored Procedures:
        /// <summary>
        /// Populates a <see cref="ComboBox"/> with items retrieved from a database using a stored procedure.
        /// </summary>
        /// <remarks>This method clears any existing items in the <see cref="ComboBox"/> before adding new
        /// items. If <paramref name="param"/> and <paramref name="value"/> are not <see langword="null"/>, they are
        /// added as parameters to the stored procedure.</remarks>
        /// <param name="combobox">The <see cref="ComboBox"/> to populate with items.</param>
        /// <param name="query">The name of the stored procedure to execute.</param>
        /// <param name="columnHeader">The name of the column whose values will be added to the <see cref="ComboBox"/>.</param>
        /// <param name="param">The name of the parameter to pass to the stored procedure. Can be <see langword="null"/> if no parameter is
        /// needed.</param>
        /// <param name="value">The value of the parameter to pass to the stored procedure. Can be <see langword="null"/> if no parameter is
        /// needed.</param>
        /// <exception cref="InvalidOperationException">Thrown if database access fails.</exception>
        public void DBGenComboBoxStoredProc(ComboBox combobox, string query, string columnHeader, string param, string value)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null && value != null)
                        {
                            cmd.Parameters.AddWithValue(param, value);
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
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBGenComboBoxStoredProc.", ex);
            }
        }
        #endregion
    }
}
