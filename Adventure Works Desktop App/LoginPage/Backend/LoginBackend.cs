using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Configuration;
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
        public AccountData accountData;

        /// <summary>
        /// Validates the provided username and password against stored account data.
        /// </summary>
        /// <remarks>If the credentials are invalid or the display name cannot be retrieved, the
        /// application will display an error message and terminate.</remarks>
        /// <param name="inputUsername">The username to validate. Cannot be null or empty.</param>
        /// <param name="inputPassword">The password to validate. Cannot be null or empty.</param>
        /// <returns><see langword="true"/> if the credentials are valid and the account data is successfully retrieved;
        /// otherwise, <see langword="false"/>.</returns>
        public bool ValidateCredentials(string inputUsername, string inputPassword)
        {
            accountData = GetLogin(inputUsername, inputPassword);

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
            return LoginDAL.DBGetDisplayName(username, password);
        }

        /// <summary>
        /// Finds out whether or not the login exists in the database.
        /// </summary>
        /// <param name="username">user entered username</param>
        /// <param name="password">user entered password</param>
        /// <returns>Data if the username and password is found</returns>
        /// <exception cref="Exception">Unable to connect to the DB</exception>
        private AccountData GetLogin(string username, string password)
        {
            return LoginDAL.DBGetAccountLoginData(username, password);
        }
    }
}
