using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App
{
    internal class EmployeeInfoSearch
    {
        Connection connect = new Connection();
        EmployeeDetails emp;
        public EmployeeInfoSearch(ComboBox comboBox, bool search)
        {
            if (search)
            {
                // pass combobox to the GetData() (bussinessentityid)
                this.emp = GetData(comboBox.Text);
            }
            else
            {
                // combobox update
                UpdateComoboBox(comboBox);
            }
        }

        public void UpdateComoboBox(ComboBox comboBox)
        {
            string query = "SELECT e.BusinessEntityID FROM HumanResources.Employee as e " +
                           "JOIN person.person AS p ON e.businessentityid = p.businessentityid " +
                           "ORDER BY e.BusinessEntityID ASC;";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, connection);
                SqlDataReader reader = queryStatus.ExecuteReader();
                comboBox.Items.Clear();
                while (reader.Read())
                {
                    string businessEntityID = $"{reader["businessEntityID"]}";
                    comboBox.Items.Add(businessEntityID);
                }
                
            }
        }

        public EmployeeDetails GetData(string businessEntityID)
        {
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                try
                {
                    connection.Open();
                    // Gets the actual data and puts it in an EmployeeDetails class
                    string query = $"execute dbo.uspGetEmployeeData @BusinessEntityID = {businessEntityID}";
                    SqlCommand queryStatus = new SqlCommand(query, connection);
                    SqlDataReader reader = queryStatus.ExecuteReader();
                    if (reader.Read())
                    {
                        EmployeeDetails emp = new EmployeeDetails($"{reader["BusinessEntityID"]}",
                            $"{reader["FirstName"]}", $"{reader["MiddleName"]}", $"{reader["LastName"]}",
                            $"{reader["JobTitle"]}", $"{reader["BirthDate"]}", $"{reader["MaritalStatus"]}",
                            $"{reader["Gender"]}", $"{reader["HireDate"]}", $"{reader["VacationHours"]}",
                            $"{reader["SickLeaveHours"]}", $"{reader["dep_name"]}", $"{reader["dep_groupname"]}",
                            $"{reader["shift_name"]}", $"{reader["Yearly_Salary"]}");
                        return emp;
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong in GetData() uspGetEmployeeData");
                    }
                    return new EmployeeDetails();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new EmployeeDetails();
                }
            }
        }

        public EmployeeDetails GetEmployeeData()
        {
            return this.emp;
        }
    }
}
