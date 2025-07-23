namespace Adventure_Works_Desktop_App.ClickerPages.Frontend
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
            this.clickerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clickerButton
            // 
            this.clickerButton.Location = new System.Drawing.Point(382, 209);
            this.clickerButton.Name = "clickerButton";
            this.clickerButton.Size = new System.Drawing.Size(75, 23);
            this.clickerButton.TabIndex = 0;
            this.clickerButton.Text = "Click me";
            this.clickerButton.UseVisualStyleBackColor = true;
            // 
            // MainClickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.clickerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainClickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clicker";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clickerButton;
    }
}