using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SignUpPage.Backend
{
    public class SignUpBackend
    {

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
        /// Inserts signup data into login table, successfully creating a new account.
        /// </summary>
        /// <param name="data">Data that is being sent to the DB.</param>
        public void SignUp(AccountData data)
        {
            string id = "";
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

                    using (SqlCommand cmd = new SqlCommand("select LoginID from Person.Login where username = @username", con))
                    {
                        cmd.Parameters.AddWithValue("@username", data.Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader != null && reader.Read())
                            {
                                id = reader["LoginID"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in SignUp.", ex);
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidOperationException("Failed to retrieve login ID after account creation");
            }

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

        /// <summary>
        /// Wrapper to check each portion of an Account to ensure valid data
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the data is a valid Account Data</returns>
        public bool ValidateData(AccountData data)
        {
            if (!ValidateUsernameDisplayName(data) || !ValidateName(data) || !ValidatePassword(data) || !ValidateEmail(data))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Uses Regex to validate a name to ensure the data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the First and Last name is valid</returns>
        private bool ValidateName(AccountData d)
        {
            const int minLength = 2;
            string regexString = Properties.AccountDataResources.RegexForName;
            Regex reg = new Regex(regexString);
            if ((reg.IsMatch(d.FirstName) && reg.IsMatch(d.LastName)) && (d.FirstName.Length >= minLength && d.LastName.Length >= minLength))
            {
                return true;
            }
            else if (!reg.IsMatch(d.FirstName) || !reg.IsMatch(d.LastName))
            {
                MessageBox.Show(Properties.AccountDataResources.InvalidMessageFirstName, Properties.AccountDataResources.TitleSignupFailed,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(Properties.AccountDataResources.InvalidMessageLastName, Properties.AccountDataResources.TitleSignupFailed,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Uses Regex to validate both a username and display name to ensure both sets of data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the username and displayname is valid</returns>
        private bool ValidateUsernameDisplayName(AccountData d)
        {
            string regexString = Properties.AccountDataResources.RegexForDisplayName;

            Regex reg = new Regex(regexString);
            if (reg.IsMatch(d.Username) && reg.IsMatch(d.DisplayName))
            {
                return true;
            }
            else if (!reg.IsMatch(d.Username))
            {
                MessageBox.Show(Properties.AccountDataResources.InvalidMessageUsername, Properties.AccountDataResources.TitleSignupFailed,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(Properties.AccountDataResources.InvalidMessageDisplayName, Properties.AccountDataResources.TitleSignupFailed,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Uses Regex to validate a password to ensure password data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the password is valid</returns>
        private bool ValidatePassword(AccountData d)
        {
            string regexString = Properties.AccountDataResources.RegexForPassword;
            Regex reg = new Regex(regexString);
            if (reg.IsMatch(d.Password))
            {
                return true;
            }
            else
            {
                MessageBox.Show(Properties.AccountDataResources.InvalidMessagePassword, Properties.AccountDataResources.TitleSignupFailed,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Uses Regex to validate an email address to ensure email data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the email is valid</returns>
        private bool ValidateEmail(AccountData d)
        {
            string regexString = Properties.AccountDataResources.RegexForEmail;
            Regex reg = new Regex(regexString);
            if (reg.IsMatch(d.Email))
            {
                return true;
            }
            else
            {
                MessageBox.Show(Properties.AccountDataResources.InvalidMessageEmail, Properties.AccountDataResources.TitleSignupFailed,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
