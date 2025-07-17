namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    partial class ChangeRegionByNameForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.enterRegionLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.regionNameTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.enterRegionLabel);
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 33);
            this.panel1.TabIndex = 6;
            // 
            // enterRegionLabel
            // 
            this.enterRegionLabel.AutoSize = true;
            this.enterRegionLabel.Location = new System.Drawing.Point(50, 10);
            this.enterRegionLabel.Name = "enterRegionLabel";
            this.enterRegionLabel.Size = new System.Drawing.Size(102, 13);
            this.enterRegionLabel.TabIndex = 0;
            this.enterRegionLabel.Text = "Enter a region name";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(67, 99);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // regionNameTextBox
            // 
            this.regionNameTextBox.Location = new System.Drawing.Point(54, 67);
            this.regionNameTextBox.Name = "regionNameTextBox";
            this.regionNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.regionNameTextBox.TabIndex = 4;
            // 
            // ChangeRegionByNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 150);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.regionNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChangeRegionByNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Region Name";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label enterRegionLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox regionNameTextBox;
    }
}