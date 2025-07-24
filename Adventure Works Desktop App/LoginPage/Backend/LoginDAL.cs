using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App.LoginPage.Backend
{
    internal class LoginDAL
    {
        #region Login Backend Data Access Layer
        // Stored Procedures:
        /// <summary>
        /// Retrieves account login data for a specified username and password.
        /// </summary>
        /// <remarks>This method uses a stored procedure to query the database for account login data.
        /// Ensure that the connection string "AdventureWorksDb" is correctly configured in the application
        /// settings.</remarks>
        /// <param name="username">The username of the account to retrieve.</param>
        /// <param name="password">The password associated with the specified username.</param>
        /// <returns>An <see cref="AccountData"/> object containing the username and password if the account is found; otherwise,
        /// an empty <see cref="AccountData"/> object.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public static  AccountData DBGetAccountLoginData(string username, string password)
        {
            AccountData data = new AccountData();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetUsernamePassword", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Username = reader["Username"].ToString();
                                data.Password = reader["Password"].ToString();
                                return data;
                            }
                            return data;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBGetAccountLoginData.", ex);
            }
        }

        // Scalar Functions:
        /// <summary>
        /// Retrieves the display name associated with the specified username and password.
        /// </summary>
        /// <param name="username">The username for which to retrieve the display name. Cannot be null or empty.</param>
        /// <param name="password">The password associated with the username. Cannot be null or empty.</param>
        /// <returns>The display name if the username and password are valid; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public static string DBGetDisplayName(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select dbo.ufnGetDisplayName(@Username, @Password)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBGetDisplayName.", ex);
            }
        }
        #endregion
    }
}
