namespace Adventure_Works_Desktop_App.Login.FrontEnd
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginButton = new System.Windows.Forms.Button();
            this.loginLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.adventureworksLabel = new System.Windows.Forms.Label();
            this.showPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.signupButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameInvalidLabel = new System.Windows.Forms.Label();
            this.passwordInvalidLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(3, 3);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(100, 23);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(1, 51);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(58, 13);
            this.loginLabel.TabIndex = 0;
            this.loginLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(3, 80);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password:";
            // 
            // adventureworksLabel
            // 
            this.adventureworksLabel.AutoSize = true;
            this.adventureworksLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adventureworksLabel.Location = new System.Drawing.Point(21, 20);
            this.adventureworksLabel.Name = "adventureworksLabel";
            this.adventureworksLabel.Size = new System.Drawing.Size(177, 25);
            this.adventureworksLabel.TabIndex = 3;
            this.adventureworksLabel.Text = "Adventure Works";
            // 
            // showPasswordCheckBox
            // 
            this.showPasswordCheckBox.AutoSize = true;
            this.showPasswordCheckBox.Location = new System.Drawing.Point(171, 79);
            this.showPasswordCheckBox.Name = "showPasswordCheckBox";
            this.showPasswordCheckBox.Size = new System.Drawing.Size(53, 17);
            this.showPasswordCheckBox.TabIndex = 4;
            this.showPasswordCheckBox.TabStop = false;
            this.showPasswordCheckBox.Text = "Show";
            this.showPasswordCheckBox.UseVisualStyleBackColor = true;
            this.showPasswordCheckBox.CheckedChanged += new System.EventHandler(this.showPasswordCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.signupButton);
            this.panel1.Controls.Add(this.loginButton);
            this.panel1.Location = new System.Drawing.Point(314, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 31);
            this.panel1.TabIndex = 1;
            // 
            // signupButton
            // 
            this.signupButton.Location = new System.Drawing.Point(109, 3);
            this.signupButton.Name = "signupButton";
            this.signupButton.Size = new System.Drawing.Size(100, 23);
            this.signupButton.TabIndex = 3;
            this.signupButton.Text = "Create Account";
            this.signupButton.UseVisualStyleBackColor = true;
            this.signupButton.Click += new System.EventHandler(this.signupButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.usernameTextBox);
            this.panel2.Controls.Add(this.loginLabel);
            this.panel2.Controls.Add(this.adventureworksLabel);
            this.panel2.Controls.Add(this.showPasswordCheckBox);
            this.panel2.Controls.Add(this.passwordLabel);
            this.panel2.Controls.Add(this.passwordTextBox);
            this.panel2.Location = new System.Drawing.Point(306, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(227, 102);
            this.panel2.TabIndex = 0;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.usernameTextBox.Location = new System.Drawing.Point(65, 48);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.usernameTextBox.TabIndex = 0;
            this.usernameTextBox.Click += new System.EventHandler(this.usernameInvalidLabel_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(65, 77);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.Click += new System.EventHandler(this.passwordInvalidLabel_Click);
            // 
            // usernameInvalidLabel
            // 
            this.usernameInvalidLabel.AutoSize = true;
            this.usernameInvalidLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usernameInvalidLabel.ForeColor = System.Drawing.Color.Red;
            this.usernameInvalidLabel.Location = new System.Drawing.Point(375, 291);
            this.usernameInvalidLabel.Name = "usernameInvalidLabel";
            this.usernameInvalidLabel.Size = new System.Drawing.Size(93, 13);
            this.usernameInvalidLabel.TabIndex = 6;
            this.usernameInvalidLabel.Text = "Username Missing";
            this.usernameInvalidLabel.Visible = false;
            this.usernameInvalidLabel.Click += new System.EventHandler(this.usernameInvalidLabel_Click);
            // 
            // passwordInvalidLabel
            // 
            this.passwordInvalidLabel.AutoSize = true;
            this.passwordInvalidLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passwordInvalidLabel.ForeColor = System.Drawing.Color.Red;
            this.passwordInvalidLabel.Location = new System.Drawing.Point(375, 291);
            this.passwordInvalidLabel.Name = "passwordInvalidLabel";
            this.passwordInvalidLabel.Size = new System.Drawing.Size(91, 13);
            this.passwordInvalidLabel.TabIndex = 5;
            this.passwordInvalidLabel.Text = "Password Missing";
            this.passwordInvalidLabel.Visible = false;
            this.passwordInvalidLabel.Click += new System.EventHandler(this.passwordInvalidLabel_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.passwordInvalidLabel);
            this.Controls.Add(this.usernameInvalidLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label adventureworksLabel;
        private System.Windows.Forms.CheckBox showPasswordCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button signupButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label passwordInvalidLabel;
        private System.Windows.Forms.Label usernameInvalidLabel;
    }
}