using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreDetailsEditBackend
    {
        enum Check
        {
            notValid = -1,
            valid = 0
        }

        /// <summary>
        /// Updates the store address given address data.
        /// </summary>
        /// <param name="addressData">Address data to submit to the database and update.</param>
        /// <exception cref="InvalidOperationException">Database could not be connected to.</exception>
        public void UpdateStoreAddress(StoreAddressData addressData)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();

                    // CountryRegionCode
                    string query = "select dbo.ufnGetCountryCode(@Name)";
                    string countryRegionCode = GetGenCode(conn, query, addressData.Country);

                    if (countryRegionCode == null)
                    {
                        MessageBox.Show("Unable to Get the country region code, application will now close");
                        Application.Exit();
                    }

                    // StateProvinceID
                    query = "select dbo.ufnGetStateProvinceID(@Name)";
                    int stateProvinceId = GetGenID(conn, query, addressData.StateProvinceName);

                    if (stateProvinceId == -1)
                    {
                        MessageBox.Show("Unable to Get the state province ID, application will now close");
                        Application.Exit();
                    }

                    // AddressTypeID
                    query = "select dbo.ufnGetAddressTypeID(@Name)";
                    int addressTypeId = GetGenID(conn, query, addressData.AddressType);

                    if (addressTypeId == -1)
                    {
                        MessageBox.Show("Unable to Get the address type id, application will now close");
                        Application.Exit();
                    }

                    // get AddressID for this BusinessEntityID and AddressTypeID
                    int addressId = 0;
                    using (SqlCommand cmd = new SqlCommand(
                        "select dbo.ufnGetAddressID(@BusinessEntityID, @AddressTypeID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@BusinessEntityID", addressData.BusinessEntityID);
                        cmd.Parameters.AddWithValue("@AddressTypeID", addressTypeId);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            addressId = (int)result;
                        }
                    }

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

        /// <summary>
        /// Updates the contact information for a store in the database.
        /// </summary>
        /// <remarks>This method updates the phone number type, phone number, email address, and email
        /// promotion status for a store contact identified by the provided business entity ID. The method throws an
        /// exception if the correct person ID cannot be found or if there is a failure in database access.</remarks>
        /// <param name="contactData">The contact data containing the updated information for the store contact, including business entity ID,
        /// phone number, email address, and promotion details.</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException">Thrown when database access fails during the update operation.</exception>
        public void UpdateStoreContact(StoreContactsData contactData)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();

                    // Get PersonID to then Get the PhoneNumber Table then update PhonenNumberTypeID with what the user gave
                    // (only options are given on the person.phonenumbertype table)
                    string query = "execute dbo.uspGetPersonIDs @BusinessEntityID = @Name";
                    List<int> personID = new List<int>();
                    List<StoreContactsData> contacts = new List<StoreContactsData>();
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

                    query = "select dbo.ufnGetPhoneNumberTypeID(@Name)";
                    int phoneNumberTypeID = GetGenID(conn, query, contactData.PhoneNumberType);

                    // now update the phonenumbertype
                    // ensure you are modifying the right one by using the contacts list and comparing it to the contactdata that passed

                    int index = 0;
                    int correctPersonID = (int)Check.notValid;
                    try
                    {
                        foreach (StoreContactsData data in contacts)
                        {
                            if (data.Title.Equals(contactData.Title) && data.FirstName.Equals(contactData.FirstName) &&
                                data.MiddleName.Equals(contactData.MiddleName) && data.LastName.Equals(contactData.LastName))
                            {
                                correctPersonID = personID[index];
                            }
                            index++;
                        }
                        if (correctPersonID == (int)Check.notValid)
                        {
                            MessageBox.Show("Not able to find person id, returning.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("COULD NOT FIND CORRECT PERSONID", ex);
                    }
                    // GenPersonUpdateOneParam(SqlConnection conn, string query, string param1, int personID, string columnName)
                    GenPersonUpdateOneParam(conn, "dbo.uspUpdatePersonPhoneNumberTypeID", $"{phoneNumberTypeID}", correctPersonID, "@ID");

                    GenPersonUpdateOneParam(conn, "dbo.uspUpdatePersonPhoneNumber", contactData.PhoneNumber, correctPersonID, "@PhoneNumber");

                    //execute uspUpdatePersonEmailAddress @PersonID = @PID, @EmailAddress = @EA
                    GenPersonUpdateOneParam(conn, "dbo.uspUpdatePersonEmailAddress", contactData.EmailAddress, correctPersonID, "@EmailAddress");

                    GenPersonUpdateOneParam(conn, "dbo.uspUpdatePersonEmailPromotion", contactData.EamilPromotion, correctPersonID, "@EmailPromotion");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in UpdateStoreContact.", ex);
            }
        }

        /// <summary>
        /// Determines whether the specified phone number type is valid by checking against the database.
        /// </summary>
        /// <remarks>This method queries the database to verify the existence of the provided phone number
        /// type. Ensure that the database connection string is correctly configured in the application
        /// settings.</remarks>
        /// <param name="phoneNumberType">The phone number type to validate.</param>
        /// <returns><see langword="true"/> if the specified phone number type exists in the database; otherwise, <see
        /// langword="false"/>.</returns>
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

        // Helper funcs
        /// <summary>
        /// Executes a SQL query to retrieve a single integer value from the specified column.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to use for executing the query. Must be open and valid.</param>
        /// <param name="query">The SQL query to execute. Should be a valid SQL statement that returns a single value.</param>
        /// <param name="columnName">The name of the column from which to retrieve the integer value.</param>
        /// <returns>The integer value from the specified column if the query succeeds and returns a result; otherwise, -1.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        private int GetGenID(SqlConnection conn, string query, string columnName)
        {
            // single get func
            try
            {
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
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetGenID.", ex);
            }
            return -1;
        }

        /// <summary>
        /// Executes a SQL query and retrieves a single value from the specified column.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to use for executing the query. Must be open before calling this method.</param>
        /// <param name="query">The SQL query to execute. The query should be structured to return a single value.</param>
        /// <param name="columnName">The name of the column from which to retrieve the value. This is used as a parameter in the query.</param>
        /// <returns>The value from the specified column as a string, or <see langword="null"/> if the query returns no results.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is an error accessing the database.</exception>
        private string GetGenCode(SqlConnection conn, string query, string columnName)
        {
            // single get func
            try
            {
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
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetGenCode.", ex);
            }
            return null;
        }

        /// <summary>
        /// Executes a stored procedure to update a specific column for a person in the database.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to use for the database operation. Must be open before calling this method.</param>
        /// <param name="query">The name of the stored procedure to execute.</param>
        /// <param name="value">The new value to set for the specified column.</param>
        /// <param name="personID">The ID of the person whose record is to be updated.</param>
        /// <param name="columnName">The name of the column to update with the new value.</param>
        /// <exception cref="InvalidOperationException">Thrown if the database access fails.</exception>
        private void GenPersonUpdateOneParam(SqlConnection conn, string query, string value, int personID, string columnName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(columnName, value);
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GenpersonUpdateOneParam.", ex);
            }
        }
    }
}
