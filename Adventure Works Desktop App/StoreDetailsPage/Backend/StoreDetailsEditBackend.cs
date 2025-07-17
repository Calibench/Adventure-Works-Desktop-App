using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreDetailsEditBackend
    {
        Connection connection = new Connection();
        public StoreDetailsEditBackend() 
        {
        }

        public void UpdateStoreAddress(StoreAddressData addressData)
        {
            using (var conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();

                // CountryRegionCode
                string query = "select dbo.ufnGetCountryCode(@Name)";
                string countryRegionCode = GetGenCode(conn, query, addressData.Country);

                // StateProvinceID
                query = "select dbo.ufnGetStateProvinceID(@Name)";
                int stateProvinceId = GetGenID(conn, query, addressData.StateProvinceName);

                // AddressTypeID
                query = "select dbo.ufnGetAddressTypeID(@Name)";
                int addressTypeId = GetGenID(conn, query, addressData.AddressType);

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
                using (SqlCommand cmd = new SqlCommand(
                    "execute dbo.uspUpdateStoreAddressInfo @AddressID = @AddID, @AddressLine1 = @AddressLine, @AddressLine2 = @AltAddressLine, " +
                    "@City = @Cit, @StateProvinceID = @StateID, @PostalCode = @PostCode", conn))
                {
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

        enum Check
        { 
            notValid = -1,
            valid = 0
        }

        public void UpdateStoreContact(StoreContactsData contactData)
        {
            using (var conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();

                // Get PersonID to then Get the PhoneNumber Table then update PhonenNumberTypeID with what the user gave
                // (only options are given on the person.phonenumbertype table
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
                                Title = $"{reader["Title"]}",
                                FirstName = $"{reader["FirstName"]}",
                                MiddleName = $"{reader["MiddleName"]}",
                                LastName = $"{reader["LastName"]}"
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

                query = "execute uspUpdatePersonPhoneNumberTypeID @ID = @PhoneNumberTypeID, @PersonID = @PID";
                GenPersonUpdateOneParam(conn, query, $"{phoneNumberTypeID}", correctPersonID, "@PhoneNumberTypeID");

                query = "execute uspUpdatePersonPhoneNumber @PersonID = @PID, @PhoneNumber = @PN";
                GenPersonUpdateOneParam(conn, query, contactData.PhoneNumber, correctPersonID, "@PN");

                query = "execute uspUpdatePersonEmailAddress @PersonID = @PID, @EmailAddress = @EA";
                GenPersonUpdateOneParam(conn, query, contactData.EmailAddress, correctPersonID, "@EA");

                query = "execute uspUpdatePersonEmailPromotion @PersonID = @PID, @EmailPromotion = @EP";
                GenPersonUpdateOneParam(conn, query, contactData.EamilPromotion, correctPersonID, "@EP");
            }
        }

        public bool ValidPhoneNumberType(string phoneNumberType)
        {
            string query = "select Name from Person.PhoneNumberType";
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (phoneNumberType.Equals($"{reader["Name"]}"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Helper funcs
        private int GetGenID(SqlConnection conn, string query, string need)
        {
            // single get func
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", need);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return (int)result;
                }
            }
            throw new Exception("Error | " + need);
        }

        private string GetGenCode(SqlConnection conn, string query, string need)
        {
            // single get func
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", need);
                var results = cmd.ExecuteScalar();
                if (results != null)
                {
                    return $"{results}";
                }
            }
            throw new Exception("Error | " + need);
        }

        private void GenPersonUpdateOneParam(SqlConnection conn, string query, string param1, int personID, string need)
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue(need, param1);
                cmd.Parameters.AddWithValue("@PID", personID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
