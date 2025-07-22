using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.LoginPage.Backend
{
    /// <summary>
    /// The backend for the login page.
    /// </summary>
    internal class LoginBackend
    {
        private Connection connection = new Connection();
        public AccountData accountData;

        /// <summary>
        /// Validates the credentials match with one in the DB.
        /// </summary>
        /// <param name="inputUsername">User entered username</param>
        /// <param name="inputPassword">User entered password</param>
        /// <returns>A <see cref="bool"/> will be returned, True=it is in the system, False=it is not</returns>
        public bool ValidateCredentials(string inputUsername, string inputPassword)
        {
            accountData = GetLoginDB(inputUsername, inputPassword);

            if (accountData.Username == null || accountData.Password == null)
            {
                return false;
            }

            if (!inputUsername.Equals(accountData.Username) || !inputPassword.Equals(accountData.Password))
            {
                return false;
            }

            accountData.DisplayName = GetDisplayName(inputUsername, inputPassword);

            if (accountData.DisplayName == null)
            {
                MessageBox.Show("Not able to login, application will now close.");
                Application.Exit();
            }

            return true; 
        }

        /// <summary>
        /// Finds the displayname based on the username and password supplied by the user.
        /// </summary>
        /// <param name="username">User entered username</param>
        /// <param name="password">User entered password</param>
        /// <returns>The display name</returns>
        /// <exception cref="Exception">Unable to connect to the DB</exception>
        private string GetDisplayName(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
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
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetDisplayName.", ex);
            }
            return null; 
        }

        /// <summary>
        /// Finds out whether or not the login exists in the database.
        /// </summary>
        /// <param name="username">user entered username</param>
        /// <param name="password">user entered password</param>
        /// <returns>Data if the username and password is found</returns>
        /// <exception cref="Exception">Unable to connect to the DB</exception>
        private AccountData GetLoginDB(string username, string password)
        {
            AccountData data = new AccountData();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetUsernamePassword", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
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
                throw new InvalidOperationException("Database access failed in GetLoginDB.", ex);
            }
        }
    }
}
