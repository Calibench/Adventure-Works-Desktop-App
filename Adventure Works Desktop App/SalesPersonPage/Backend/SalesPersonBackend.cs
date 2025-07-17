using Adventure_Works_Desktop_App.Globals;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Backend
{
    internal class SalesPersonBackend
    {
        Connection connection = new Connection();

        private string id;
        public string Id
        { 
            get { return id; }
        }

        public bool ValidateID(string id)
        {
            return Val(SearchDBForID(id));
        }

        public bool ValidateName(string name)
        {
            return Val(SearchDBForName(name));
        }

        private string SearchDBForID(string id)
        {
            string query = "select BusinessEntityID from Sales.SalesPerson where BusinessEntityID = @ID";
            return GenQuery(query, "BusinessEntityID", "@ID", id);
        }

        private string SearchDBForName(string name)
        {
            string query = "select sp.BusinessEntityID, (FirstName + ' ' + LastName) as full_name from Sales.SalesPerson as sp " +
                           "join Person.Person as p on sp.BusinessEntityID = p.BusinessEntityID " +
                           "where (FirstName + ' ' + LastName) = @name;";
            return GenQuery(query, "BusinessEntityID", "@name", name);
        }

        public SalesPersonData GetSalesPersonData(string id)
        {
            RegionData regionData = new RegionData();
            SalesPersonData salesPersonData = new SalesPersonData();

            // testing using SqlCommand's built in StoredProcedure func.
            // string query = "execute dbo.uspGetSalesPersonData @ID = @userID";

            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("uspGetSalesPersonData", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            regionData.RegionName = $"{reader["Region_Name"]}";
                            regionData.RegionCode = $"{reader["CountryRegionCode"]}";
                            regionData.TerritoryID = $"{reader["TerritoryID"]}";
                            regionData.Continent = $"{reader["Continent"]}";
                            regionData.TotalSalesYTD = $"{reader["Total_SalesYTD"]}";
                            regionData.TotalSalesLY = $"{reader["Total_SalesYTD_LastYear"]}";

                            salesPersonData.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                            salesPersonData.SalesQuota = $"{reader["SalesQuota"]}";
                            salesPersonData.Bonus = $"{reader["Bonus"]}";
                            salesPersonData.CommissionPct = $"{reader["CommissionPct"]}";
                            salesPersonData.SalesYTD = $"{reader["SalesYTD"]}";
                            salesPersonData.SalesLY = $"{reader["SalesLastYear"]}";
                            salesPersonData.FirstName = $"{reader["FirstName"]}";
                            salesPersonData.LastName = $"{reader["LastName"]}";
                            salesPersonData.RegionData = regionData;
                        }
                    }
                }
            }
            return salesPersonData;
        }

        // Region Specific
        public bool RegionIsValid(string name)
        {

            string temp = SearchDbForRegionName(name);
            return Val(temp);
        }

        public string SearchDbForRegionName(string name)
        {
            string query = "select st.Name from sales.SalesTerritory as st where st.Name = @name";
            return GenQuery(query, "Name", "@name", name);
        }

        public RegionData GetRegionData(string name)
        { 
            RegionData regionData = new RegionData();

            string query = "select * from sales.SalesTerritory where Name = @Name";

            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            regionData.RegionName = $"{reader["Name"]}";
                            regionData.RegionCode = $"{reader["CountryRegionCode"]}";
                            regionData.TerritoryID = $"{reader["TerritoryID"]}";
                            regionData.Continent = $"{reader["Group"]}";
                            regionData.TotalSalesYTD = $"{reader["SalesYTD"]}";
                            regionData.TotalSalesLY = $"{reader["SalesLastYear"]}";
                        }
                    }
                }
            }

            return regionData;
        }

        // Helper Methods
        private bool Val(string temp)
        {
            id = temp;
            if (temp != null && temp.Length >= 1)
            {
                return true;
            }
            return false;
        }

        private string GenQuery(string query, string need, string param, string data)
        {
            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue(param, data);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return $"{reader[need]}";
                }
            }
            return "";
        }
    }
}
