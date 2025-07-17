using Adventure_Works_Desktop_App.Globals.DataClasses;
using Adventure_Works_Desktop_App.SignUpPage.Backend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SignUpPage.Frontend
{
    public partial class SignUpForm : Form
    {
        SignUpBackend backend = new SignUpBackend();
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            AccountData data = new AccountData(firstNameTextBox.Text, lastNameTextBox.Text,
                                   usernameTextBox.Text, passwordTextBox.Text, displayNameTextBox.Text, emailTextBox.Text);
            
            if (!backend.ValidateData(data))
            {
                return;
            }

            if (backend.CheckUnique(data))
            {
                backend.SignUp(data);
                this.Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
