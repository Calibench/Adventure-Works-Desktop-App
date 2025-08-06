namespace Clicker.ClickerPages.Frontend
{
    partial class MainClickerForm
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
            this.clickMeButton = new System.Windows.Forms.Button();
            this.numOfClicksLabel = new System.Windows.Forms.Label();
            this.numOfClicksPanels = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numOfClicksPanels.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // clickMeButton
            // 
            this.clickMeButton.Location = new System.Drawing.Point(63, 33);
            this.clickMeButton.Name = "clickMeButton";
            this.clickMeButton.Size = new System.Drawing.Size(75, 23);
            this.clickMeButton.TabIndex = 0;
            this.clickMeButton.Text = "Click me";
            this.clickMeButton.UseVisualStyleBackColor = true;
            this.clickMeButton.Click += new System.EventHandler(this.clickMeButton_Click);
            // 
            // numOfClicksLabel
            // 
            this.numOfClicksLabel.AutoSize = true;
            this.numOfClicksLabel.Location = new System.Drawing.Point(91, 6);
            this.numOfClicksLabel.Name = "numOfClicksLabel";
            this.numOfClicksLabel.Size = new System.Drawing.Size(0, 13);
            this.numOfClicksLabel.TabIndex = 1;
            // 
            // numOfClicksPanels
            // 
            this.numOfClicksPanels.Controls.Add(this.numOfClicksLabel);
            this.numOfClicksPanels.Location = new System.Drawing.Point(3, 3);
            this.numOfClicksPanels.Name = "numOfClicksPanels";
            this.numOfClicksPanels.Size = new System.Drawing.Size(194, 24);
            this.numOfClicksPanels.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numOfClicksPanels);
            this.panel2.Controls.Add(this.clickMeButton);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 60);
            this.panel2.TabIndex = 3;
            // 
            // MainClickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainClickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clicker";
            this.numOfClicksPanels.ResumeLayout(false);
            this.numOfClicksPanels.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clickMeButton;
        private System.Windows.Forms.Label numOfClicksLabel;
        private System.Windows.Forms.Panel numOfClicksPanels;
        private System.Windows.Forms.Panel panel2;
    }
}