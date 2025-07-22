using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            try
            {
                string query = "execute dbo.uspSearchStoreByID @BusinessEntityID = @ID";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool specificData = true;
                            AddData(specificData, reader, addressDataSingle, contactsDataSingle, demographicsDataSingle);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetSpecificData.", ex);
            }
        }

        // Used in the list
        private void GetAllAddressData()
        {
            try
            {
                string query = "select * from sales.vStoreWithAddresses order by BusinessEntityID";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StoreAddressData temp = new StoreAddressData();

                                temp.BusinessEntityID = reader["BusinessEntityID"].ToString();
                                temp.StoreName = reader["Name"].ToString();
                                temp.AddressType = reader["AddressType"].ToString();
                                temp.AddressLine1 = reader["AddressLine1"].ToString();
                                temp.AddressLine2 = reader["AddressLine2"].ToString();
                                temp.City = reader["City"].ToString();
                                temp.StateProvinceName = reader["StateProvinceName"].ToString();
                                temp.PostalCode = reader["PostalCode"].ToString();
                                temp.Country = reader["CountryRegionName"].ToString();

                                addressData.Add(temp);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetAllAddressData.", ex);
            }
        }

        private void GetAllContactsData()
        {
            try
            {
                string query = "select * from sales.vStoreWithContacts order by BusinessEntityID";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StoreContactsData temp = new StoreContactsData();

                                temp.BusinessEntityID = reader["BusinessEntityID"].ToString();
                                temp.StoreName = reader["Name"].ToString();
                                temp.ContactType = reader["ContactType"].ToString();
                                temp.Title = reader["Title"].ToString();
                                temp.FirstName = reader["FirstName"].ToString();
                                temp.MiddleName = reader["MiddleName"].ToString();
                                temp.LastName = reader["LastName"].ToString();
                                temp.Suffix = reader["Suffix"].ToString();
                                temp.PhoneNumber = reader["PhoneNumber"].ToString();
                                temp.PhoneNumberType = reader["PhoneNumberType"].ToString();
                                temp.EmailAddress = reader["EmailAddress"].ToString();
                                temp.EamilPromotion = reader["EmailPromotion"].ToString();

                                contactsData.Add(temp);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetAllContactsData.", ex);
            }
        }

        private void GetAllDemographicsData()
        {
            try
            {
                string query = "select * from sales.vStoreWithDemographics order by BusinessEntityID";
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StoreDemographicsData temp = new StoreDemographicsData();

                                temp.BusinessEntityID = reader["BusinessEntityID"].ToString();
                                temp.StoreName = reader["Name"].ToString();
                                temp.AnnualSales = reader["AnnualSales"].ToString();
                                temp.AnnualRevenue = reader["AnnualRevenue"].ToString();
                                temp.BankName = reader["BankName"].ToString();
                                temp.BusinessType = reader["BusinessType"].ToString();
                                temp.YearOpened = reader["YearOpened"].ToString();
                                temp.Specialty = reader["Specialty"].ToString();
                                temp.SquareFeet = reader["SquareFeet"].ToString();
                                temp.Brands = reader["Brands"].ToString();
                                temp.Internet = reader["Internet"].ToString();
                                temp.NumberOfEmployees = reader["NumberEmployees"].ToString();

                                demographicsData.Add(temp);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetAllDemoGraphicsData.", ex);
            }
        }

        public void SortedByStoreName(string userSearchText)
        {
            string query = "dbo.SearchStoreName";
            string param = "@StoreName";
            string value = userSearchText;
            SortedByStore(query, param, value);
        }

        public void SortedByStoreCity(string userSearchText)
        {
            string query = "dbo.SearchStoreCity";
            string param = "@City";
            string value = userSearchText;
            SortedByStore(query, param, value);
        }

        public void SortedByStoreCountry(string userSearchText)
        {
            string query = "dbo.SearchStoreCountry";
            string param = "@Country";
            string value = userSearchText;
            SortedByStore(query, param, value);
        }

        public void SortedByStoreYear(string userSearchText)
        {
            string query = "dbo.SearchStoreYear";
            string param = "@Year";
            string value = userSearchText;
            SortedByStore(query, param, value);
        }

        private void SortedByStore(string query, string param, string value)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(param, value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool specificData = false;
                            AddData(specificData, reader, addressData, contactsData, demographicsData);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in SortedByStore.", ex);
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

                tempAddress.BusinessEntityID = reader["BusinessEntityID"].ToString();
                tempAddress.StoreName = reader["Name"].ToString();
                tempAddress.AddressType = reader["AddressType"].ToString();
                tempAddress.AddressLine1 = reader["AddressLine1"].ToString();
                tempAddress.AddressLine2 = reader["AddressLine2"].ToString();
                tempAddress.City = reader["City"].ToString();
                tempAddress.StateProvinceName = reader["StateProvinceName"].ToString();
                tempAddress.PostalCode = reader["PostalCode"].ToString();
                tempAddress.Country = reader["CountryRegionName"].ToString();

                if (specific)
                {
                    tempContacts.BusinessEntityID = reader["BusinessEntityID"].ToString();
                    tempContacts.StoreName = reader["Name"].ToString();
                    tempContacts.ContactType = reader["ContactType"].ToString();
                    tempContacts.Title = reader["Title"].ToString();
                    tempContacts.FirstName = reader["FirstName"].ToString();
                    tempContacts.MiddleName = reader["MiddleName"].ToString();
                    tempContacts.LastName = reader["LastName"].ToString();
                    tempContacts.Suffix = reader["Suffix"].ToString();
                    tempContacts.PhoneNumber = reader["PhoneNumber"].ToString();
                    tempContacts.PhoneNumberType = reader["PhoneNumberType"].ToString();
                    tempContacts.EmailAddress = reader["EmailAddress"].ToString();
                    tempContacts.EamilPromotion = reader["EmailPromotion"].ToString();
                }

                tempDemographic.BusinessEntityID = reader["BusinessEntityID"].ToString();
                tempDemographic.StoreName = reader["Name"].ToString();
                tempDemographic.AnnualSales = reader["AnnualSales"].ToString();
                tempDemographic.AnnualRevenue = reader["AnnualRevenue"].ToString();
                tempDemographic.BankName = reader["BankName"].ToString();
                tempDemographic.BusinessType = reader["BusinessType"].ToString();
                tempDemographic.YearOpened = reader["YearOpened"].ToString();
                tempDemographic.Specialty = reader["Specialty"].ToString();
                tempDemographic.SquareFeet = reader["SquareFeet"].ToString();
                tempDemographic.Brands = reader["Brands"].ToString();
                tempDemographic.Internet = reader["Internet"].ToString();
                tempDemographic.NumberOfEmployees = reader["NumberEmployees"].ToString();

                addData.Add(tempAddress);
                conData.Add(tempContacts);
                demData.Add(tempDemographic);
            }
        }
    }
}
