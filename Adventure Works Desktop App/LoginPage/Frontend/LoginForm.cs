using Adventure_Works_Desktop_App.Globals;
using Adventure_Works_Desktop_App.LoginPage.Backend;
using Adventure_Works_Desktop_App.MenuPage;
using Adventure_Works_Desktop_App.SignUpPage.Frontend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.Login.FrontEnd
{
    public partial class LoginForm : Form
    {
        // Class Global Variables
        private enum PasswordProtect
        {
            invisiblePassword = '●',
            visiblePassword = '\0'
        }

        // Constructor
        public LoginForm()
        {
            InitializeComponent();
        }

        // Event Driven Methods
        private void loginButton_Click(object sender, EventArgs e)
        {
            ButtonInverter(loginButton); // Ensures that the button cannot be double clicked by accident

            if (!CheckLoginFields())
            {
                ButtonInverter(loginButton);
                return;
            }

            Login();
        }

        private void showPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPasswordCheckBox.Checked)
            {
                passwordTextBox.PasswordChar = (char)PasswordProtect.visiblePassword;
            }
            else
            {
                passwordTextBox.PasswordChar = (char)PasswordProtect.invisiblePassword;
            }
        }

        private void signupButton_Click(object sender, EventArgs e)
        {
            ButtonInverter(signupButton); // Ensures that the button cannot be double clicked by accident

            SignUpForm signUpForm = new SignUpForm();
            FormNavigationHelper.ShowFormAndHideCurrent(this, signUpForm);

            ButtonInverter(signupButton);
        }

        private void passwordInvalidLabel_Click(object sender, EventArgs e)
        {
            ClearFocusTextBox(passwordTextBox, passwordInvalidLabel);
        }

        private void usernameInvalidLabel_Click(object sender, EventArgs e)
        {
            ClearFocusTextBox(usernameTextBox, usernameInvalidLabel);
        }

        // Helper Methods
        /// <summary>
        /// Login handler
        /// </summary>
        private void Login()
        {
            try
            {
                LoginBackend backend = new LoginBackend();
                if (backend.ValidateCredentials(usernameTextBox.Text, passwordTextBox.Text))
                {
                    // For accessing these message strings go to Properties -> Resources.resx in Solutions Explorer
                    MessageBox.Show(string.Format(Properties.LoginPageResources.SuccessMessageLogin, backend.accountData.DisplayName));

                    MenuForm menuForm = new MenuForm(backend.accountData.DisplayName);
                    FormNavigationHelper.ShowFormAndCloseCurrent(this, menuForm);
                }
                else
                {
                    MessageBox.Show(Properties.LoginPageResources.InvalidMessageCredentials, Properties.LoginPageResources.TitleFailedLogin,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.LoginPageResources.ErrorMessageLogin + ex, Properties.LoginPageResources.TitleError,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ButtonInverter(loginButton); // Allow the button to be clicked again
        }

        /// <summary>
        /// Sets focus to the specified <see cref="TextBox"/> and hides the associated <see cref="Label"/>.
        /// </summary>
        /// <param name="textBox">The <see cref="TextBox"/> to receive focus. Cannot be null.</param>
        /// <param name="label">The <see cref="Label"/> to be hidden. Cannot be null.</param>
        private void ClearFocusTextBox(TextBox textBox, Label label)
        {
            textBox.Focus();
            label.Visible = false;
        }

        /// <summary>
        /// Toggles the enabled state of the specified button.
        /// </summary>
        /// <param name="button">The button whose enabled state is to be inverted. Cannot be <see langword="null"/>.</param>
        private void ButtonInverter(Button button)
        {
            button.Enabled = !button.Enabled;
        }

        /// <summary>
        /// Validates the login fields to ensure that both username and password are provided.
        /// </summary>
        /// <remarks>If the username field is empty, the username invalid label is made visible.
        /// Similarly, if the password field is empty, the password invalid label is made visible.</remarks>
        /// <returns><see langword="true"/> if both the username and password fields are filled; otherwise, <see
        /// langword="false"/>.</returns>
        private bool CheckLoginFields()
        {
            if (usernameTextBox.Text.Equals(""))
            {
                usernameInvalidLabel.Visible = true;
                return false;
            }
            else if (passwordTextBox.Text.Equals(""))
            {
                passwordInvalidLabel.Visible = true;
                return false;
            }
            return true;
        }
    }
}
