using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    /// <summary>
    /// This backend handle the employee info page that displays data about an employee to the user.
    /// </summary>
    internal class EmployeeInfoBackend
    {
        Connection connect = new Connection();

        /// <summary>
        /// Gets employee ID's and populates them into a given combobox.
        /// </summary>
        /// <param name="comboBox">Combobox that is needs to be populated.</param>
        public void UpdateComoboBox(ComboBox comboBox)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetAllEmployeeIDs", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBox.Items.Clear();
                            while (reader.Read())
                            {
                                string businessEntityID = reader["businessEntityID"].ToString();
                                comboBox.Items.Add(businessEntityID);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in UpdateComboBox.", ex);
            }
        }

        /// <summary>
        /// Given an employee ID, return data on the employee.
        /// </summary>
        /// <param name="businessEntityID">The employee ID.</param>
        /// <returns></returns>
        public EmployeeData GetData(string businessEntityID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connect.ConnectionString))
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
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetData.", ex);
            }
            MessageBox.Show(Properties.Resources.ErrorMessageConnectionLost);
            Application.Exit();
            return null;
        }
    }
}
