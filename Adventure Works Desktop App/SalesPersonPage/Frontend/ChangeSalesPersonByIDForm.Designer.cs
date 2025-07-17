namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    partial class ChangeSalesPersonByIDForm
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
            this.enterIDLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // enterIDLabel
            // 
            this.enterIDLabel.AutoSize = true;
            this.enterIDLabel.Location = new System.Drawing.Point(71, 10);
            this.enterIDLabel.Name = "enterIDLabel";
            this.enterIDLabel.Size = new System.Drawing.Size(61, 13);
            this.enterIDLabel.TabIndex = 0;
            this.enterIDLabel.Text = "Enter an ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(56, 60);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(69, 92);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.enterIDLabel);
            this.panel1.Location = new System.Drawing.Point(5, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 33);
            this.panel1.TabIndex = 3;
            // 
            // ChangeSalesPersonByIDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 150);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChangeSalesPersonByIDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change By *";
            this.Load += new System.EventHandler(this.InitialFormLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enterIDLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Panel panel1;
    }
}