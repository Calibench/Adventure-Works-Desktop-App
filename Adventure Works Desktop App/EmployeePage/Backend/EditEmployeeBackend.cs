using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    internal class EditEmployeeBackend
    {
        private Connection connect = new Connection();
        private string id;
        public EditEmployeeBackend(string id) 
        {
            this.id = id;
        }

        public void FillItems(ComboBox combobox, string type, string extra)
        {
            switch (type)
            {
                case "Job":
                    FillItemsJob(combobox, extra);
                    break;
                case "Name":
                    FillItemsName(combobox, extra);
                    break;
                case "Group":
                    FillItemsGroup(combobox);
                    break;
                case "Shift":
                    FillItemsShift(combobox);
                    break;
                default:
                    throw new ArgumentException("Invalid type of combobox", nameof(type));
            }
        }

        private void FillItemsJob(ComboBox combobox, string deptName)
        {
            string query = "select distinct JobTitle from HumanResources.Employee as e " +
                           "join HumanResources.EmployeeDepartmentHistory as edh on e.BusinessEntityID = edh.BusinessEntityID " +
                           "join HumanResources.Department as d on edh.DepartmentID = d.DepartmentID " +
                           "where d.Name = @DeptName";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, connection);
                queryStatus.Parameters.AddWithValue("@DeptName", deptName);
                SqlDataReader reader = queryStatus.ExecuteReader();
                combobox.Items.Clear();
                while (reader.Read())
                {
                    string item = $"{reader["JobTitle"]}";
                    combobox.Items.Add(item);
                }
                return;
            }
            throw new ArgumentException("Error with Fetching Item", nameof(query));
        }

        private void FillItemsName(ComboBox combobox, string groupName)
        {
            string query = "select name from HumanResources.Department where GroupName = @GroupName";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, connection);
                queryStatus.Parameters.AddWithValue("@GroupName", groupName);
                SqlDataReader reader = queryStatus.ExecuteReader();
                combobox.Items.Clear();
                while (reader.Read())
                {
                    string item = $"{reader["name"]}";
                    combobox.Items.Add(item);
                }
                return;
            }
            throw new ArgumentException("Error with Fetching Item", nameof(query));
        }

        private void FillItemsGroup(ComboBox combobox)
        {
            string query = "select distinct GroupName from HumanResources.Department";
            GenComboFill(combobox, query, "GroupName");
        }

        private void FillItemsShift(ComboBox combobox)
        {
            string query = "select Name from HumanResources.Shift";
            GenComboFill(combobox, query, "Name");
        }

        private void GenComboFill(ComboBox combobox, string query, string columnHeader)
        {
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, connection);
                SqlDataReader reader = queryStatus.ExecuteReader();
                combobox.Items.Clear();
                while (reader.Read())
                {
                    string item = $"{reader[columnHeader]}";
                    combobox.Items.Add(item);
                }
            }
        }

        public void PushToSql(EmployeeDetails data)
        {
            UpdateBasicEmployeeInfo(data);
            UpdateBasicPersonInfo(data);
            UpdateEmployeeDepartmentHistory(data);
            UpdateEmployeePayHistory(data);
        }

        private void UpdateBasicEmployeeInfo(EmployeeDetails data)
        {
            string query = "update HumanResources.Employee " +
                           "set JobTitle=@JobTitle, BirthDate=@BirthDate, MaritalStatus=@MaritalStatus, Gender=@Gender, " +
                           "HireDate=@HireDate, VacationHours=@VacationHours, SickLeaveHours=@SickLeaveHours " +
                           "where BusinessEntityID = @ID";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@JobTitle", data.GetJobTitle());
                cmd.Parameters.AddWithValue("@BirthDate", data.GetBirthDate());
                cmd.Parameters.AddWithValue("@MaritalStatus", data.GetMaritalStatus());
                cmd.Parameters.AddWithValue("@Gender", data.GetGender());
                cmd.Parameters.AddWithValue("@HireDate", data.GetHireDate());
                cmd.Parameters.AddWithValue("@VacationHours", data.GetVacationHours());
                cmd.Parameters.AddWithValue("@SickLeaveHours", data.GetSickLeaveHours());
                cmd.Parameters.AddWithValue("ID", data.GetBusinessEntityID());
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateBasicPersonInfo(EmployeeDetails data)
        {
            string query = "update Person.Person " +
                           "set FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName " +
                           "where BusinessEntityID = @ID";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FirstName", data.GetFirstName());
                cmd.Parameters.AddWithValue("@MiddleName", data.GetMiddleName());
                cmd.Parameters.AddWithValue("@LastName", data.GetLastName());
                cmd.Parameters.AddWithValue("@ID", data.GetBusinessEntityID());
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployeeDepartmentHistory(EmployeeDetails data)
        {
            string[] ids = new string[2];
            ids[0] = FindDepartmentDetail(data);
            ids[1] = FindShiftDetails(data);

            string query = "update HumanResources.EmployeeDepartmentHistory " +
                           "set DepartmentID=@DepartmentID, ShiftID=@ShiftID " +
                           "where BusinessEntityID = @ID";

            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentID", ids[0]);
                cmd.Parameters.AddWithValue("@ShiftID", ids[1]);
                cmd.Parameters.AddWithValue("@ID", data.GetBusinessEntityID());
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployeePayHistory(EmployeeDetails data)
        {
            decimal reverseSalaryToRate = decimal.Parse(data.GetYearlySalary());
            reverseSalaryToRate /= 52; // 52 weeks
            reverseSalaryToRate /= 40; // 40 hours
            string salaryRate = $"{reverseSalaryToRate}";

            if (!CheckSalaryChange(salaryRate, data))
            {
                return;
            }

            DateTime date = DateTime.Now;

            string payFreq = GetPayFreq(data);

            string query = "insert into HumanResources.EmployeePayHistory (BusinessEntityID, RateChangeDate, Rate, PayFrequency, ModifiedDate)" +
                           "values (@BusinessEntityID, @RateChangeDate, @NewRate, @PayFrequency, @ModifiedDate)";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@BusinessEntityID", data.GetBusinessEntityID());
                cmd.Parameters.AddWithValue("@RateChangeDate", date.ToString());
                cmd.Parameters.AddWithValue("@NewRate", salaryRate);
                cmd.Parameters.AddWithValue("@PayFrequency", payFreq);
                cmd.Parameters.AddWithValue("@ModifiedDate", date.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private bool CheckSalaryChange(string salaryRate, EmployeeDetails data)
        {
            string query = "select Rate from HumanResources.EmployeePayHistory where BusinessEntityID = @ID";
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", data.GetBusinessEntityID());
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ($"{reader["Rate"]}".Equals(salaryRate))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string GetPayFreq(EmployeeDetails data)
        {
            string query = "select PayFrequency from HumanResources.EmployeePayHistory where BusinessEntityID = @BusinessEntityID";
            return GenFetchOneItem(query, "@BusinessEntityID", data.GetBusinessEntityID(), "PayFrequency");
        }

        private string FindDepartmentDetail(EmployeeDetails data)
        {
            string query = "select d.DepartmentID " +
                           "from HumanResources.Department as d " +
                           "where d.Name = @Name";
            return GenFetchOneItem(query, "@Name", data.GetDepartmentName(), "DepartmentID");
        }

        private string FindShiftDetails(EmployeeDetails data)
        {
            string query = "select s.ShiftID " +
                           "from HumanResources.Shift as s " +
                           "where s.Name = @Name";
            return GenFetchOneItem(query, "@Name", data.GetShiftName(), "ShiftID");
        }

        private string GenFetchOneItem(string query, string param, string data, string need)
        {
            using (SqlConnection connection = new SqlConnection(connect.ConnectionString))
            {
                connection.Open();
                SqlCommand queryStatus = new SqlCommand(query, connection);
                queryStatus.Parameters.AddWithValue(param, data);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    return $"{reader[need]}";
                }
            }
            throw new ArgumentException("Error with Fetching Item", nameof(query));
        }
    }
}
