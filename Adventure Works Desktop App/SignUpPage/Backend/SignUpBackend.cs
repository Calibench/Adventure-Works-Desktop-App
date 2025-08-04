using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SignUpPage.Backend
{
    public class SignUpBackend
    {
        SignUpDAL _dal = new SignUpDAL();

        /// <summary>
        /// Checks whether the username is already in use by comparing it to the login table.
        /// </summary>
        /// <param name="data">Account data to check the username from.</param>
        /// <returns>Returns a <see cref="bool">, of whether the username is currently in use or not.</cref></returns>
        public bool CheckUnique(AccountData data)
        {
            return _dal.CheckUnique(data);
        }

        /// <summary>
        /// Inserts signup data into login table, successfully creating a new account.
        /// </summary>
        /// <param name="data">Data that is being sent to the DB.</param>
        public void SignUp(AccountData data)
        {
            string id = "";
            _dal.SignUp(data); // regular signup to adventureworks db
            id = _dal.GetID(data); // get id for clicker signup if the signup was successful.

            if (string.IsNullOrEmpty(id))
            {
                throw new InvalidOperationException("Failed to retrieve login ID after account creation");
            }

            _dal.ClickerSignUp(id); // for clicker db
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
