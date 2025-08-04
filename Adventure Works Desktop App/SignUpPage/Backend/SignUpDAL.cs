using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App.SignUpPage.Backend
{
    internal class SignUpDAL
    {
        #region Sign Up Backend

        #region Stored Procedures:

        /// <summary>
        /// Creates a new user account by inserting the provided account data into the database.
        /// </summary>
        /// <remarks>
        /// This method uses a stored procedure named <c>dbo.uspInsertNewAccount</c> to insert the account data. 
        /// Ensure that the database connection string is correctly configured in the application's configuration file.
        /// </remarks>
        /// <param name="data">An <see cref="AccountData"/> object containing the details of the account to be created,  including first
        /// name, last name, username, password, display name, and email.</param>
        /// <exception cref="InvalidOperationException">Thrown when a database access error occurs during the account creation process. The inner exception contains
        /// details about the underlying <see cref="SqlException"/>.</exception>
        public void SignUp(AccountData data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspInsertNewAccount", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", data.LastName);
                        cmd.Parameters.AddWithValue("@Username", data.Username);
                        cmd.Parameters.AddWithValue("@Password", data.Password);
                        cmd.Parameters.AddWithValue("@DisplayName", data.DisplayName);
                        cmd.Parameters.AddWithValue("@Email", data.Email);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in SignUp.", ex);
            }
        }

        /// <summary>
        /// Registers a new user in the Clicker system by adding their login ID to the database.
        /// </summary>
        /// <remarks>
        /// This method establishes a connection to the database, inserts the provided login ID
        /// into the ClickerUserData table, and ensures proper disposal of database resources. 
        /// Ensure that the connection string "Clicker" is correctly configured in the application's configuration file.
        /// </remarks>
        /// <param name="id">The login ID of the user to be registered. This value cannot be null or empty.</param>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs during the registration process. The inner exception contains
        /// details about the underlying <see cref="System.Data.SqlClient.SqlException"/>.</exception>
        public void ClickerSignUp(string id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Clicker"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("insert into ClickerUserData(LoginID) values(@LoginID)", con))
                    {
                        cmd.Parameters.AddWithValue("@LoginID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in SignUp.", ex);
            }
        }

        #endregion

        #region Scalar Functions:

        /// <summary>
        /// Checks whether the username is already in use by comparing it to the login table.
        /// </summary>
        /// <param name="data">Account data to check the username from.</param>
        /// <returns>Returns a <see cref="bool">, of whether the username is currently in use or not.</cref></returns>
        public bool CheckUnique(AccountData data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.ufnCheckUserName(@GivenUsername)", conn))
                    {
                        cmd.Parameters.AddWithValue("@GivenUsername", data.Username);
                        var result = cmd.ExecuteScalar();
                        if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                        {
                            return false; // it is not unique
                        }
                        return true; // it is unique
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in CheckUnique.", ex);
            }
        }

        /// <summary>
        /// Retrieves the Login ID associated with the specified account data.
        /// </summary>
        /// <remarks>
        /// This method queries the database using the provided username to retrieve the corresponding Login ID.
        /// Ensure that the database connection string is correctly configured in the application's configuration file.
        /// </remarks>
        /// <param name="data">The account data containing the username for which the Login ID is to be retrieved. The <see
        /// cref="AccountData.Username"/> property must not be null or empty.</param>
        /// <returns>The Login ID as a <see cref="string"/> if the username is found in the database; otherwise, <see
        /// langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if a database access error occurs while attempting to retrieve the Login ID.</exception>
        public string GetID(AccountData data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select dbo.ufnGetLoginIDFromUsername(@username)", con))
                    {
                        cmd.Parameters.AddWithValue("@username", data.Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.Read())
                            {
                                return reader["LoginID"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in SignUp.", ex);
            }
            return null;
        }

        #endregion

        #endregion
    }
}
