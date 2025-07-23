namespace Adventure_Works_Desktop_App.MenuPage
{
    partial class ProtectForm
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
            this.codeLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.oneButton = new System.Windows.Forms.Button();
            this.twoButton = new System.Windows.Forms.Button();
            this.threeButton = new System.Windows.Forms.Button();
            this.fourButton = new System.Windows.Forms.Button();
            this.fiveButton = new System.Windows.Forms.Button();
            this.sixButton = new System.Windows.Forms.Button();
            this.sevenButton = new System.Windows.Forms.Button();
            this.eightButton = new System.Windows.Forms.Button();
            this.nineButton = new System.Windows.Forms.Button();
            this.zeroButton = new System.Windows.Forms.Button();
            this.incorrectCodeLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(165, 38);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(62, 13);
            this.codeLabel.TabIndex = 0;
            this.codeLabel.Text = "Enter code:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.oneButton);
            this.flowLayoutPanel1.Controls.Add(this.twoButton);
            this.flowLayoutPanel1.Controls.Add(this.threeButton);
            this.flowLayoutPanel1.Controls.Add(this.fourButton);
            this.flowLayoutPanel1.Controls.Add(this.fiveButton);
            this.flowLayoutPanel1.Controls.Add(this.sixButton);
            this.flowLayoutPanel1.Controls.Add(this.sevenButton);
            this.flowLayoutPanel1.Controls.Add(this.eightButton);
            this.flowLayoutPanel1.Controls.Add(this.nineButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(74, 54);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(244, 90);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // oneButton
            // 
            this.oneButton.Location = new System.Drawing.Point(3, 3);
            this.oneButton.Name = "oneButton";
            this.oneButton.Size = new System.Drawing.Size(75, 23);
            this.oneButton.TabIndex = 0;
            this.oneButton.Tag = "1";
            this.oneButton.Text = "1";
            this.oneButton.UseVisualStyleBackColor = true;
            this.oneButton.Click += new System.EventHandler(this.AddCode);
            // 
            // twoButton
            // 
            this.twoButton.Location = new System.Drawing.Point(84, 3);
            this.twoButton.Name = "twoButton";
            this.twoButton.Size = new System.Drawing.Size(75, 23);
            this.twoButton.TabIndex = 1;
            this.twoButton.Tag = "2";
            this.twoButton.Text = "2";
            this.twoButton.UseVisualStyleBackColor = true;
            this.twoButton.Click += new System.EventHandler(this.AddCode);
            // 
            // threeButton
            // 
            this.threeButton.Location = new System.Drawing.Point(165, 3);
            this.threeButton.Name = "threeButton";
            this.threeButton.Size = new System.Drawing.Size(75, 23);
            this.threeButton.TabIndex = 2;
            this.threeButton.Tag = "3";
            this.threeButton.Text = "3";
            this.threeButton.UseVisualStyleBackColor = true;
            this.threeButton.Click += new System.EventHandler(this.AddCode);
            // 
            // fourButton
            // 
            this.fourButton.Location = new System.Drawing.Point(3, 32);
            this.fourButton.Name = "fourButton";
            this.fourButton.Size = new System.Drawing.Size(75, 23);
            this.fourButton.TabIndex = 3;
            this.fourButton.Tag = "4";
            this.fourButton.Text = "4";
            this.fourButton.UseVisualStyleBackColor = true;
            this.fourButton.Click += new System.EventHandler(this.AddCode);
            // 
            // fiveButton
            // 
            this.fiveButton.Location = new System.Drawing.Point(84, 32);
            this.fiveButton.Name = "fiveButton";
            this.fiveButton.Size = new System.Drawing.Size(75, 23);
            this.fiveButton.TabIndex = 4;
            this.fiveButton.Tag = "5";
            this.fiveButton.Text = "5";
            this.fiveButton.UseVisualStyleBackColor = true;
            this.fiveButton.Click += new System.EventHandler(this.AddCode);
            // 
            // sixButton
            // 
            this.sixButton.Location = new System.Drawing.Point(165, 32);
            this.sixButton.Name = "sixButton";
            this.sixButton.Size = new System.Drawing.Size(75, 23);
            this.sixButton.TabIndex = 5;
            this.sixButton.Tag = "6";
            this.sixButton.Text = "6";
            this.sixButton.UseVisualStyleBackColor = true;
            this.sixButton.Click += new System.EventHandler(this.AddCode);
            // 
            // sevenButton
            // 
            this.sevenButton.Location = new System.Drawing.Point(3, 61);
            this.sevenButton.Name = "sevenButton";
            this.sevenButton.Size = new System.Drawing.Size(75, 23);
            this.sevenButton.TabIndex = 6;
            this.sevenButton.Tag = "7";
            this.sevenButton.Text = "7";
            this.sevenButton.UseVisualStyleBackColor = true;
            this.sevenButton.Click += new System.EventHandler(this.AddCode);
            // 
            // eightButton
            // 
            this.eightButton.Location = new System.Drawing.Point(84, 61);
            this.eightButton.Name = "eightButton";
            this.eightButton.Size = new System.Drawing.Size(75, 23);
            this.eightButton.TabIndex = 7;
            this.eightButton.Tag = "8";
            this.eightButton.Text = "8";
            this.eightButton.UseVisualStyleBackColor = true;
            this.eightButton.Click += new System.EventHandler(this.AddCode);
            // 
            // nineButton
            // 
            this.nineButton.Location = new System.Drawing.Point(165, 61);
            this.nineButton.Name = "nineButton";
            this.nineButton.Size = new System.Drawing.Size(75, 23);
            this.nineButton.TabIndex = 8;
            this.nineButton.Tag = "9";
            this.nineButton.Text = "9";
            this.nineButton.UseVisualStyleBackColor = true;
            this.nineButton.Click += new System.EventHandler(this.AddCode);
            // 
            // zeroButton
            // 
            this.zeroButton.Location = new System.Drawing.Point(158, 144);
            this.zeroButton.Name = "zeroButton";
            this.zeroButton.Size = new System.Drawing.Size(75, 23);
            this.zeroButton.TabIndex = 2;
            this.zeroButton.Tag = "0";
            this.zeroButton.Text = "0";
            this.zeroButton.UseVisualStyleBackColor = true;
            this.zeroButton.Click += new System.EventHandler(this.AddCode);
            // 
            // incorrectCodeLabel
            // 
            this.incorrectCodeLabel.AutoSize = true;
            this.incorrectCodeLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.incorrectCodeLabel.Location = new System.Drawing.Point(157, 170);
            this.incorrectCodeLabel.Name = "incorrectCodeLabel";
            this.incorrectCodeLabel.Size = new System.Drawing.Size(77, 13);
            this.incorrectCodeLabel.TabIndex = 3;
            this.incorrectCodeLabel.Text = "Incorrect Code";
            this.incorrectCodeLabel.Visible = false;
            // 
            // ProtectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 205);
            this.Controls.Add(this.incorrectCodeLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.zeroButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProtectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clicker Code";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button oneButton;
        private System.Windows.Forms.Button twoButton;
        private System.Windows.Forms.Button threeButton;
        private System.Windows.Forms.Button fourButton;
        private System.Windows.Forms.Button fiveButton;
        private System.Windows.Forms.Button sixButton;
        private System.Windows.Forms.Button sevenButton;
        private System.Windows.Forms.Button eightButton;
        private System.Windows.Forms.Button nineButton;
        private System.Windows.Forms.Button zeroButton;
        private System.Windows.Forms.Label incorrectCodeLabel;
    }
}