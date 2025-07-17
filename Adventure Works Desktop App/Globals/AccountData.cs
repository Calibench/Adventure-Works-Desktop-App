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
        /// A constructor when just needing to contain username and password. Mainly used for Login.
        /// </summary>
        /// <param name="username">A given user's username</param>
        /// <param name="password">A given user's password</param>
        public AccountData(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Wrapper to check each portion of an Account to ensure valid data
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the data is a valid Account Data</returns>
        public bool ValidateData()
        {
            if (!ValidateName() || !ValidateUsernameDisplayName() || !ValidatePassword() || !ValidateEmail())
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
            // a valid string "tyler.'- ross"
            string regexString = "^[a-zA-Z ,-]{2,50}$";
            Regex reg = new Regex(regexString);
            if ((reg.IsMatch(FirstName) && reg.IsMatch(LastName)) && (FirstName.Length >= minLength && LastName.Length >= minLength))
            {
                return true;
            }
            else if (!reg.IsMatch(FirstName) || !reg.IsMatch(LastName))
            {
                MessageBox.Show($"{FirstName} is an invalid first name.\nOnly:(a-z | -) and between 2-50 characters");
            }
            else 
            {
                MessageBox.Show($"{LastName} is an invalid last name.\nOnly:(a-z | -) and between 2-50 characters");
            }
                return false;
        }

        /// <summary>
        /// Uses Regex to validate both a username and display name to ensure both sets of data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the username and displayname is valid</returns>
        private bool ValidateUsernameDisplayName()
        {
            string regexString = "^[a-zA-Z][a-zA-Z0-9_-]{3,15}$";

            Regex reg = new Regex(regexString);
            if (reg.IsMatch(Username) && reg.IsMatch(DisplayName))
            {
                return true;
            }
            else if (!reg.IsMatch(Username))
            {
                MessageBox.Show($"Invalid Username.\nOnly:(Letters, Numbers, Underscore, Hyphens)\nMust start with a letter");
            }
            else
            {
                MessageBox.Show($"Invalid Display Name.\nOnly:(Letters, Numbers, Underscore, Hyphens)\nMust start with a letter");
            }
            return false;
        }

        /// <summary>
        /// Uses Regex to validate a password to ensure password data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the password is valid</returns>
        private bool ValidatePassword()
        {
            // regex = (https://ihateregex.io/expr/password/) (5-50 characters long)
            string regexString = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,50}$";
            Regex reg = new Regex(regexString);
            if (reg.IsMatch(Password))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Password must contain:\none uppercase letter\none lowercase letter" +
                    "\none number\none special character\nbe at least 5 characters long");
            }
            return false;
        }

        /// <summary>
        /// Uses Regex to validate an email address to ensure email data is correct before storing
        /// </summary>
        /// <returns>Returns a <see cref="bool"> on whether the email is valid</returns>
        private bool ValidateEmail()
        {
            string regexString = "[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+";
            Regex reg = new Regex(regexString);
            if (reg.IsMatch(Email))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Email address not accepted.\nPlease enter a different one.");
            }
            return false;
        }
    }
}
