using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreDetailsDAL
    {

        #region Store Details Backend

        #region Stored Procedures:

        /// <summary>
        /// Retrieves specific store data by ID and populates the provided lists with address, contact, 
        /// and demographic information.
        /// </summary>
        /// <remarks>
        /// This method executes a stored procedure named <c>dbo.uspSearchStoreByID</c> to retrieve the store data. 
        /// The provided lists are cleared and then populated with the retrieved data.
        /// </remarks>
        /// <param name="id">The unique identifier of the store to retrieve data for. Cannot be null or empty.</param>
        /// <param name="addressDataSingle">A list to be populated with the store's address data. Must not be null.</param>
        /// <param name="contactsDataSingle">A list to be populated with the store's contact data. Must not be null.</param>
        /// <param name="demographicsDataSingle">A list to be populated with the store's demographic data. Must not be null.</param>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs while retrieving the store data.</exception>
        public void GetSpecificData(string id, List<StoreAddressData> addressDataSingle, List<StoreContactsData> contactsDataSingle, List<StoreDemographicsData> demographicsDataSingle)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspSearchStoreByID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BusinessEntityID", id);
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

        /// <summary>
        /// Populates the provided list with address data for all stores from the database.
        /// </summary>
        /// <remarks>
        /// This method retrieves all store address data from the "sales.vStoreWithAddresses"
        /// view in the database and orders the results by BusinessEntityID. 
        /// Each record is mapped to a <see cref="StoreAddressData"/> object and added to the provided list.
        /// </remarks>
        /// <param name="addressData">A list to which the method will add <see cref="StoreAddressData"/> objects representing store address
        /// information. The list must be initialized before calling this method.</param>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs. The inner exception will contain details about the SQL error.</exception>
        public void GetAllAddressData(List<StoreAddressData> addressData)
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

        /// <summary>
        /// Populates the provided list with contact data from the database.
        /// </summary>
        /// <remarks>
        /// This method retrieves all contact data from the "sales.vStoreWithContacts" view in the database and orders the results by BusinessEntityID. 
        /// The provided list is populated with instances of <see cref="StoreContactsData"/> representing each contact.
        /// </remarks>
        /// <param name="contactsData">A list to be populated with contact data. Each entry in the list represents a contact, including details
        /// such as business entity ID, store name, contact type, and personal information.</param>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs. The inner exception contains details about the SQL error.</exception>
        public void GetAllContactsData(List<StoreContactsData> contactsData)
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

        /// <summary>
        /// Populates the provided list with demographic data for all stores.
        /// </summary>
        /// <remarks>
        /// This method retrieves data from the "sales.vStoreWithDemographics" view in the
        /// AdventureWorks database and orders the results by the BusinessEntityID. 
        /// The provided list is populated with one entry per store.
        /// </remarks>
        /// <param name="demographicsData">A list to be populated with <see cref="StoreDemographicsData"/> objects, each representing the demographic
        /// data of a store.</param>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs. The inner exception contains details about the SQL error.</exception>
        public void GetAllDemographicsData(List<StoreDemographicsData> demographicsData)
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

        /// <summary>
        /// Executes a stored procedure to retrieve and populate store-related data, such as addresses, contacts, and
        /// demographics.
        /// </summary>
        /// <remarks>
        /// This method connects to the database using the connection string specified in the application's configuration file. 
        /// It executes the specified stored procedure with the provided parameter and value, 
        /// and processes the resulting data to populate the provided lists. 
        /// Ensure that the connection string "AdventureWorksDb" is correctly configured.
        /// </remarks>
        /// <param name="query">The name of the stored procedure to execute.</param>
        /// <param name="param">The name of the parameter to pass to the stored procedure.</param>
        /// <param name="value">The value of the parameter to pass to the stored procedure.</param>
        /// <param name="addressData">A list to populate with store address data retrieved from the database.</param>
        /// <param name="contactsData">A list to populate with store contact data retrieved from the database.</param>
        /// <param name="demographicsData">A list to populate with store demographic data retrieved from the database.</param>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs while executing the stored procedure.</exception>
        public void SortedByStore(string query, string param, string value,
                                  List<StoreAddressData> addressData, List<StoreContactsData> contactsData, List<StoreDemographicsData> demographicsData)
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

        #endregion

        #region Helper Methods:

        /// <summary>
        /// Populates the provided lists with store-related data from the specified <see cref="SqlDataReader"/>.
        /// </summary>
        /// <remarks>
        /// This method reads all rows from the <paramref name="reader"/> and populates the provided lists with  corresponding data. 
        /// Ensure that the <paramref name="reader"/> contains the expected columns for
        /// <see cref="StoreAddressData"/>, <see cref="StoreContactsData"/>, and <see cref="StoreDemographicsData"/>.
        /// </remarks>
        /// <param name="specific">A boolean value indicating whether to include detailed contact information.  If <see langword="true"/>,
        /// contact data will be populated in addition to address and demographic data.</param>
        /// <param name="reader">The <see cref="SqlDataReader"/> containing the data to be read.  The reader must be positioned before the
        /// first record.</param>
        /// <param name="addData">A list to be populated with <see cref="StoreAddressData"/> objects representing store address information.</param>
        /// <param name="conData">A list to be populated with <see cref="StoreContactsData"/> objects representing store contact information. 
        /// This list will only be populated if <paramref name="specific"/> is <see langword="true"/>.</param>
        /// <param name="demData">A list to be populated with <see cref="StoreDemographicsData"/> objects representing store demographic
        /// information.</param>
        private void AddData(bool specific, SqlDataReader reader,
                                  List<StoreAddressData> addData, List<StoreContactsData> conData, List<StoreDemographicsData> demData)
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

        #endregion

        #endregion

        #region Store Details Edit Backend

        #region Stored Procedures:

        /// <summary>
        /// Executes a stored procedure to update a specific column for a person in the database.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to use for the database operation. Must be open before calling this method.</param>
        /// <param name="query">The name of the stored procedure to execute.</param>
        /// <param name="value">The new value to set for the specified column.</param>
        /// <param name="personID">The ID of the person whose record is to be updated.</param>
        /// <param name="columnName">The name of the column to update with the new value.</param>
        /// <exception cref="InvalidOperationException">Thrown if the database access fails.</exception>
        public void GenPersonUpdateOneParam(string query, string value, int personID, string columnName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(columnName, value);
                        cmd.Parameters.AddWithValue("@PersonID", personID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GenpersonUpdateOneParam.", ex);
            }
        }

        public void GetPersonInformationID(string query, List<int> personID, List<StoreContactsData> contacts, StoreContactsData contactData)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", contactData.BusinessEntityID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                personID.Add((int)reader["PersonID"]);
                                StoreContactsData temp = new StoreContactsData
                                {
                                    Title = reader["Title"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    LastName = reader["LastName"].ToString()
                                };
                                contacts.Add(temp);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetPersonInformationID.", ex);
            }
        }

        public void UpdateAddress(StoreAddressData addressData, int stateProvinceId, int addressId)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    // update Address
                    using (SqlCommand cmd = new SqlCommand("dbo.uspUpdateStoreAddressInfo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AddressLine1", addressData.AddressLine1 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@AddressLine2", addressData.AddressLine2 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", addressData.City);
                        cmd.Parameters.AddWithValue("@StateProvinceID", stateProvinceId);
                        cmd.Parameters.AddWithValue("@PostalCode", addressData.PostalCode);
                        cmd.Parameters.AddWithValue("@AddressID", addressId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in UpdateStoreAddress.", ex);
            }
        }

        #endregion

        #region Scalar Functions:

        /// <summary>
        /// Determines whether the specified phone number type is valid by checking against the database.
        /// </summary>
        /// <remarks>
        /// This method queries the database to verify the existence of the provided phone number type. 
        /// Ensure that the database connection string is correctly configured in the application settings.
        /// </remarks>
        /// <param name="phoneNumberType">The phone number type to validate.</param>
        /// <returns><see langword="true"/> if the specified phone number type exists in the database; otherwise, 
        /// <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public bool ValidPhoneNumberType(string phoneNumberType)
        {
            try
            {
                string query = "select Name from Person.PhoneNumberType";
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (phoneNumberType.Equals(reader["Name"].ToString()))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in ValidPhoneNumber.", ex);
            }
            return false;
        }

        public int GetAddressID(StoreAddressData addressData, int addressTypeId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                                "select dbo.ufnGetAddressID(@BusinessEntityID, @AddressTypeID)", conn))
                {
                    cmd.Parameters.AddWithValue("@BusinessEntityID", addressData.BusinessEntityID);
                    cmd.Parameters.AddWithValue("@AddressTypeID", addressTypeId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return (int)result;
                    }
                } 
            }
            return -1;
        }

        #endregion

        #region Helper Methods:

        /// <summary>
        /// Executes a SQL query and retrieves a single value from the specified column.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to use for executing the query. Must be open before calling this method.</param>
        /// <param name="query">The SQL query to execute. The query should be structured to return a single value.</param>
        /// <param name="columnName">The name of the column from which to retrieve the value. This is used as a parameter in the query.</param>
        /// <returns>The value from the specified column as a string, or <see langword="null"/> if the query returns no results.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is an error accessing the database.</exception>
        public string GetGenCode(string query, string columnName)
        {
            // single get func
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", columnName);
                        var results = cmd.ExecuteScalar();
                        if (results != null)
                        {
                            return results.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetGenCode.", ex);
            }
            return null;
        }

        /// <summary>
        /// Executes a SQL query to retrieve a single integer value from the specified column.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to use for executing the query. Must be open and valid.</param>
        /// <param name="query">The SQL query to execute. Should be a valid SQL statement that returns a single value.</param>
        /// <param name="columnName">The name of the column from which to retrieve the integer value.</param>
        /// <returns>The integer value from the specified column if the query succeeds and returns a result; otherwise, -1.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public int GetGenID(string query, string columnName)
        {
            // single get func
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", columnName);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return (int)result;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetGenID.", ex);
            }
            return -1;
        }

        #endregion

        #endregion
    }
}
