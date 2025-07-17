using Adventure_Works_Desktop_App.Globals;
using Adventure_Works_Desktop_App.SignUpPage.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SignUpPage.Frontend
{
    public partial class SignUpForm : Form
    {
        public bool backButtonPressed = false;
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            AccountData data = new AccountData(firstNameTextBox.Text, lastNameTextBox.Text,
                                   usernameTextBox.Text, passwordTextBox.Text, displayNameTextBox.Text, emailTextBox.Text);
            if (!data.ValidateData())
            {
                return;
            }
            SignUpBackend backend = new SignUpBackend();
            if (backend.CheckUnique(data))
            {
                backend.SignUp(data);
                backButtonPressed = true;
                this.Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            backButtonPressed = true;
            this.Close();
        }
    }
}
