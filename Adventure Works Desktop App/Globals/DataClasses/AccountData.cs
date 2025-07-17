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
    }
}
