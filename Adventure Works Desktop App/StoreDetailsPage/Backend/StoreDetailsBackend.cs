using System.Collections.Generic;
using System.Data.SqlClient;
namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreDetailsBackend
    {
        public List<StoreAddressData> addressData;
        public List<StoreContactsData> contactsData;
        public List<StoreDemographicsData> demographicsData;

        public List<StoreAddressData> addressDataSingle;
        public List<StoreContactsData> contactsDataSingle;
        public List<StoreDemographicsData> demographicsDataSingle;
        private string id;

        Connection connect = new Connection();
        public StoreDetailsBackend() 
        {
            addressData = new List<StoreAddressData>();
            contactsData = new List<StoreContactsData>();
            demographicsData = new List<StoreDemographicsData>();
        }

        public StoreDetailsBackend(string id)
        {
            addressDataSingle = new List<StoreAddressData>();
            contactsDataSingle = new List<StoreContactsData>();
            demographicsDataSingle = new List<StoreDemographicsData>();
            this.id = id;
        }

        // Used in Details form to get specifics (through ID)
        public void GetSpecificData()
        {
            string query = "execute dbo.uspSearchStoreByID @BusinessEntityID = @ID";
            using (SqlConnection connection = new SqlConnection(connect.GetConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                bool specificData = true;
                AddData(specificData, reader, addressDataSingle, contactsDataSingle, demographicsDataSingle);
            }
        }

        // Used in the list
        private void GetAllAddressData()
        {
            string query = "select * from sales.vStoreWithAddresses order by BusinessEntityID";
            using (SqlConnection connection = new SqlConnection(connect.GetConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StoreAddressData temp = new StoreAddressData();

                    temp.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                    temp.StoreName = $"{reader["Name"]}";
                    temp.AddressType = $"{reader["AddressType"]}";
                    temp.AddressLine1 = $"{reader["AddressLine1"]}";
                    temp.AddressLine2 = $"{reader["AddressLine2"]}";
                    temp.City = $"{reader["City"]}";
                    temp.StateProvinceName = $"{reader["StateProvinceName"]}";
                    temp.PostalCode = $"{reader["PostalCode"]}";
                    temp.Country = $"{reader["CountryRegionName"]}";

                    addressData.Add(temp);
                }
            }
        }

        private void GetAllContactsData()
        {

            string query = "select * from sales.vStoreWithContacts order by BusinessEntityID";
            using (SqlConnection connection = new SqlConnection(connect.GetConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StoreContactsData temp = new StoreContactsData();

                    temp.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                    temp.StoreName = $"{reader["Name"]}";
                    temp.ContactType = $"{reader["ContactType"]}";
                    temp.Title = $"{reader["Title"]}";
                    temp.FirstName = $"{reader["FirstName"]}";
                    temp.MiddleName = $"{reader["MiddleName"]}";
                    temp.LastName = $"{reader["LastName"]}";
                    temp.Suffix = $"{reader["Suffix"]}";
                    temp.PhoneNumber = $"{reader["PhoneNumber"]}";
                    temp.PhoneNumberType = $"{reader["PhoneNumberType"]}";
                    temp.EmailAddress = $"{reader["EmailAddress"]}";
                    temp.EamilPromotion = $"{reader["EmailPromotion"]}";

                    contactsData.Add(temp);
                }
            }
        }

        private void GetAllDemographicsData()
        {

            string query = "select * from sales.vStoreWithDemographics order by BusinessEntityID";
            using (SqlConnection connection = new SqlConnection(connect.GetConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StoreDemographicsData temp = new StoreDemographicsData();

                    temp.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                    temp.StoreName = $"{reader["Name"]}";
                    temp.AnnualSales = $"{reader["AnnualSales"]}";
                    temp.AnnualRevenue = $"{reader["AnnualRevenue"]}";
                    temp.BankName = $"{reader["BankName"]}";
                    temp.BusinessType = $"{reader["BusinessType"]}";
                    temp.YearOpened = $"{reader["YearOpened"]}";
                    temp.Specialty = $"{reader["Specialty"]}";
                    temp.SquareFeet = $"{reader["SquareFeet"]}";
                    temp.Brands = $"{reader["Brands"]}";
                    temp.Internet = $"{reader["Internet"]}";
                    temp.NumberOfEmployees = $"{reader["NumberEmployees"]}";

                    demographicsData.Add(temp);
                }
            }
        }

        public void SortedByStoreName(string userSearchText)
        {
            string query = "execute dbo.SearchStoreName @StoreName = @SearchText";
            string param = userSearchText;
            SortedByStore(query, param);
        }

        public void SortedByStoreCity(string userSearchText)
        {
            string query = "execute dbo.SearchStoreCity @City = @SearchText";
            string param = userSearchText;
            SortedByStore(query, param);
        }

        public void SortedByStoreCountry(string userSearchText)
        {
            string query = "execute dbo.SearchStoreCountry @Country = @SearchText";
            string param = userSearchText;
            SortedByStore(query, param);
        }

        public void SortedByStoreYear(string userSearchText)
        {
            string query = "execute dbo.SearchStoreYear @Year = @SearchText";
            string param = userSearchText;
            SortedByStore(query, param);
        }

        private void SortedByStore(string query, string param)
        {
            using (SqlConnection connection = new SqlConnection(connect.GetConnectionString()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchText", param);
                SqlDataReader reader = cmd.ExecuteReader();
                bool specificData = false;
                AddData(specificData, reader, addressData, contactsData, demographicsData);
            }
        }

        // Helper Methods
        public void GetAllStoreDetailList()
        {
            GetAllAddressData();
            GetAllContactsData();
            GetAllDemographicsData();
        }

        private void AddData(bool specific, SqlDataReader reader, List<StoreAddressData> addData, 
                            List<StoreContactsData> conData, List<StoreDemographicsData> demData)
        {
            while (reader.Read())
            {
                StoreAddressData tempAddress = new StoreAddressData();
                StoreContactsData tempContacts = new StoreContactsData();
                StoreDemographicsData tempDemographic = new StoreDemographicsData();

                tempAddress.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                tempAddress.StoreName = $"{reader["Name"]}";
                tempAddress.AddressType = $"{reader["AddressType"]}";
                tempAddress.AddressLine1 = $"{reader["AddressLine1"]}";
                tempAddress.AddressLine2 = $"{reader["AddressLine2"]}";
                tempAddress.City = $"{reader["City"]}";
                tempAddress.StateProvinceName = $"{reader["StateProvinceName"]}";
                tempAddress.PostalCode = $"{reader["PostalCode"]}";
                tempAddress.Country = $"{reader["CountryRegionName"]}";

                if (specific) 
                {
                    tempContacts.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                    tempContacts.StoreName = $"{reader["Name"]}";
                    tempContacts.ContactType = $"{reader["ContactType"]}";
                    tempContacts.Title = $"{reader["Title"]}";
                    tempContacts.FirstName = $"{reader["FirstName"]}";
                    tempContacts.MiddleName = $"{reader["MiddleName"]}";
                    tempContacts.LastName = $"{reader["LastName"]}";
                    tempContacts.Suffix = $"{reader["Suffix"]}";
                    tempContacts.PhoneNumber = $"{reader["PhoneNumber"]}";
                    tempContacts.PhoneNumberType = $"{reader["PhoneNumberType"]}";
                    tempContacts.EmailAddress = $"{reader["EmailAddress"]}";
                    tempContacts.EamilPromotion = $"{reader["EmailPromotion"]}";
                }

                tempDemographic.BusinessEntityID = $"{reader["BusinessEntityID"]}";
                tempDemographic.StoreName = $"{reader["Name"]}";
                tempDemographic.AnnualSales = $"{reader["AnnualSales"]}";
                tempDemographic.AnnualRevenue = $"{reader["AnnualRevenue"]}";
                tempDemographic.BankName = $"{reader["BankName"]}";
                tempDemographic.BusinessType = $"{reader["BusinessType"]}";
                tempDemographic.YearOpened = $"{reader["YearOpened"]}";
                tempDemographic.Specialty = $"{reader["Specialty"]}";
                tempDemographic.SquareFeet = $"{reader["SquareFeet"]}";
                tempDemographic.Brands = $"{reader["Brands"]}";
                tempDemographic.Internet = $"{reader["Internet"]}";
                tempDemographic.NumberOfEmployees = $"{reader["NumberEmployees"]}";

                addData.Add(tempAddress);
                conData.Add(tempContacts);
                demData.Add(tempDemographic);
            }
        }
    }
}
