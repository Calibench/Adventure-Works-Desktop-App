namespace Adventure_Works_Desktop_App.MenuPage
{
    partial class MenuForm
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
            this.employeeInfoButton = new System.Windows.Forms.Button();
            this.menuTitleLabel = new System.Windows.Forms.Label();
            this.productsInfoButton = new System.Windows.Forms.Button();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.addReviewButton = new System.Windows.Forms.Button();
            this.salesInfoButton = new System.Windows.Forms.Button();
            this.viewStoreDetailsButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.clickerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // employeeInfoButton
            // 
            this.employeeInfoButton.Location = new System.Drawing.Point(378, 150);
            this.employeeInfoButton.Name = "employeeInfoButton";
            this.employeeInfoButton.Size = new System.Drawing.Size(83, 23);
            this.employeeInfoButton.TabIndex = 1;
            this.employeeInfoButton.Text = "Employee Info";
            this.employeeInfoButton.UseVisualStyleBackColor = true;
            this.employeeInfoButton.Click += new System.EventHandler(this.employeeInfoButton_Click);
            // 
            // menuTitleLabel
            // 
            this.menuTitleLabel.AutoSize = true;
            this.menuTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuTitleLabel.Location = new System.Drawing.Point(386, 122);
            this.menuTitleLabel.Name = "menuTitleLabel";
            this.menuTitleLabel.Size = new System.Drawing.Size(66, 25);
            this.menuTitleLabel.TabIndex = 0;
            this.menuTitleLabel.Text = "Menu";
            // 
            // productsInfoButton
            // 
            this.productsInfoButton.Location = new System.Drawing.Point(378, 179);
            this.productsInfoButton.Name = "productsInfoButton";
            this.productsInfoButton.Size = new System.Drawing.Size(83, 23);
            this.productsInfoButton.TabIndex = 2;
            this.productsInfoButton.Text = "Products Info";
            this.productsInfoButton.UseVisualStyleBackColor = true;
            this.productsInfoButton.Click += new System.EventHandler(this.productsInfoButton_Click);
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Location = new System.Drawing.Point(379, 428);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(80, 13);
            this.welcomeLabel.TabIndex = 3;
            this.welcomeLabel.Text = "Welcome, User";
            // 
            // addReviewButton
            // 
            this.addReviewButton.Location = new System.Drawing.Point(378, 208);
            this.addReviewButton.Name = "addReviewButton";
            this.addReviewButton.Size = new System.Drawing.Size(83, 23);
            this.addReviewButton.TabIndex = 4;
            this.addReviewButton.Text = "Add Review";
            this.addReviewButton.UseVisualStyleBackColor = true;
            this.addReviewButton.Click += new System.EventHandler(this.addReviewButton_Click);
            // 
            // salesInfoButton
            // 
            this.salesInfoButton.Location = new System.Drawing.Point(378, 237);
            this.salesInfoButton.Name = "salesInfoButton";
            this.salesInfoButton.Size = new System.Drawing.Size(83, 23);
            this.salesInfoButton.TabIndex = 5;
            this.salesInfoButton.Text = "Sales Info";
            this.salesInfoButton.UseVisualStyleBackColor = true;
            this.salesInfoButton.Click += new System.EventHandler(this.salesInfoButton_Click);
            // 
            // viewStoreDetailsButton
            // 
            this.viewStoreDetailsButton.Location = new System.Drawing.Point(378, 266);
            this.viewStoreDetailsButton.Name = "viewStoreDetailsButton";
            this.viewStoreDetailsButton.Size = new System.Drawing.Size(83, 23);
            this.viewStoreDetailsButton.TabIndex = 6;
            this.viewStoreDetailsButton.Text = "View Store Details";
            this.viewStoreDetailsButton.UseVisualStyleBackColor = true;
            this.viewStoreDetailsButton.Click += new System.EventHandler(this.viewStoreDetailsButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(378, 295);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(83, 23);
            this.exitButton.TabIndex = 7;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // clickerButton
            // 
            this.clickerButton.Location = new System.Drawing.Point(828, 431);
            this.clickerButton.Name = "clickerButton";
            this.clickerButton.Size = new System.Drawing.Size(10, 10);
            this.clickerButton.TabIndex = 8;
            this.clickerButton.TabStop = false;
            this.clickerButton.UseVisualStyleBackColor = true;
            this.clickerButton.Click += new System.EventHandler(this.clickerButton_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.clickerButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.viewStoreDetailsButton);
            this.Controls.Add(this.salesInfoButton);
            this.Controls.Add(this.addReviewButton);
            this.Controls.Add(this.welcomeLabel);
            this.Controls.Add(this.productsInfoButton);
            this.Controls.Add(this.menuTitleLabel);
            this.Controls.Add(this.employeeInfoButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.InitalFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button employeeInfoButton;
        private System.Windows.Forms.Label menuTitleLabel;
        private System.Windows.Forms.Button productsInfoButton;
        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Button addReviewButton;
        private System.Windows.Forms.Button salesInfoButton;
        private System.Windows.Forms.Button viewStoreDetailsButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button clickerButton;
    }
}