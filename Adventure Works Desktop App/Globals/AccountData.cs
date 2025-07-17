using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.Globals
{
    /// <summary>
    /// Account Data is used to store account data. Is used in Login and Signup. 
    /// </summary>
    public class AccountData
    {
        // Properties
        public string FirstName
        { get; set; }

        public string LastName
        { get; set; }

        public string Username
        { get; set; }

        public string Password
        { get; set; }

        public string DisplayName
        { get; set; }

        public string Email
        { get; set; }

        /// <summary>
        /// Empty account data constructor
        /// </summary>
        public AccountData()
        {

        }

        /// <summary>
        /// A constructor to ensure all portions of Account Data is filled. Mainly used for Signup.
        /// </summary>
        /// <param name="firstName">A given user's first name</param>
        /// <param name="lastName">A given user's last name</param>
        /// <param name="username">A given user's username</param>
        /// <param name="password">A given user's password</param>
        /// <param name="displayName">A given user's display name</param>
        /// <param name="email">A given user's email</param>
        public AccountData(string firstName, string lastName, string username, string password, string displayName, string email) 
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            DisplayName = displayName;
            Email = email;
        }

        /// <summary>
        /// Wrapper to check each portion of an Account to ensure valid data
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the data is a valid Account Data</returns>
        public bool ValidateData()
        {
            if (!ValidateUsernameDisplayName() || !ValidateName() || !ValidatePassword() || !ValidateEmail())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Uses Regex to validate a name to ensure the data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the First and Last name is valid</returns>
        private bool ValidateName()
        {
            const int minLength = 2;
            string regexString = Properties.AccountDataResources.RegexForName;
            Regex reg = new Regex(regexString);
            if ((reg.IsMatch(FirstName) && reg.IsMatch(LastName)) && (FirstName.Length >= minLength && LastName.Length >= minLength))
            {
                return true;
            }
            else if (!reg.IsMatch(FirstName) || !reg.IsMatch(LastName))
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
        private bool ValidateUsernameDisplayName()
        {
            string regexString = Properties.AccountDataResources.RegexForDisplayName;

            Regex reg = new Regex(regexString);
            if (reg.IsMatch(Username) && reg.IsMatch(DisplayName))
            {
                return true;
            }
            else if (!reg.IsMatch(Username))
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
        private bool ValidatePassword()
        {
            string regexString = Properties.AccountDataResources.RegexForPassword;
            Regex reg = new Regex(regexString);
            if (reg.IsMatch(Password))
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
        private bool ValidateEmail()
        {
            string regexString = Properties.AccountDataResources.RegexForEmail;
            Regex reg = new Regex(regexString);
            if (reg.IsMatch(Email))
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
