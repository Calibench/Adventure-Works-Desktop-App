using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
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
                        cmd.Parameters.AddWithValue("@AddressLine", addressData.AddressLine1 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@AltAddressLine", addressData.AddressLine2 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cit", addressData.City);
                        cmd.Parameters.AddWithValue("@StateID", stateProvinceId);
                        cmd.Parameters.AddWithValue("@PostCode", addressData.PostalCode);
                        cmd.Parameters.AddWithValue("@AddID", addressId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in UpdateStoreAddress.", ex);
            }
        }

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
                        throw new Exception("COULD NOT FIND CORRECT PERSONID");
                    }

                    GenPersonUpdateOneParam(conn, "uspUpdatePersonPhoneNumberTypeID", $"{phoneNumberTypeID}", correctPersonID, "@PhoneNumberTypeID");

                    GenPersonUpdateOneParam(conn, "uspUpdatePersonPhoneNumber", contactData.PhoneNumber, correctPersonID, "@PN");

                    //execute uspUpdatePersonEmailAddress @PersonID = @PID, @EmailAddress = @EA
                    GenPersonUpdateOneParam(conn, "uspUpdatePersonEmailAddress", contactData.EmailAddress, correctPersonID, "@EA");

                    GenPersonUpdateOneParam(conn, "uspUpdatePersonEmailPromotion", contactData.EamilPromotion, correctPersonID, "@EP");
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in UpdateStoreContact.", ex);
            }
        }

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

        private void GenPersonUpdateOneParam(SqlConnection conn, string query, string param1, int personID, string columnName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(columnName, param1);
                    cmd.Parameters.AddWithValue("@PID", personID);
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
