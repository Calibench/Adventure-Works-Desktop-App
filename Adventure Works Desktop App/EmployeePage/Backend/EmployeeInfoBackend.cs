using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    internal class EmployeeInfoBackend
    {
        Connection connect = new Connection();
        private EmployeeData Emp
        { get; set; }

        public EmployeeInfoBackend()//ComboBox comboBox, bool search)
        {
            //if (search)
            //{
            //    // pass combobox to the GetData() (bussinessentityid)
            //    Emp = GetData(comboBox.Text);
            //}
            //else
            //{
            //    // combobox update
            //    UpdateComoboBox(comboBox);
            //}
        }

        // done
        public void UpdateComoboBox(ComboBox comboBox)
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

        public EmployeeData GetData(string businessEntityID)
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
            MessageBox.Show("Something went wrong in GetData() uspGetEmployeeData");
            return null;
        }

        public EmployeeData GetEmployeeData()
        {
            return Emp;
        }
    }
}
