namespace Adventure
{
    partial class MainForm
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
            this.hitPointsLabel = new System.Windows.Forms.Label();
            this.goldLabel = new System.Windows.Forms.Label();
            this.experienceLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.hitPointsValueLabel = new System.Windows.Forms.Label();
            this.goldValueLabel = new System.Windows.Forms.Label();
            this.experienceValueLabel = new System.Windows.Forms.Label();
            this.levelValueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hitPointsLabel
            // 
            this.hitPointsLabel.AutoSize = true;
            this.hitPointsLabel.Location = new System.Drawing.Point(16, 16);
            this.hitPointsLabel.Name = "hitPointsLabel";
            this.hitPointsLabel.Size = new System.Drawing.Size(55, 13);
            this.hitPointsLabel.TabIndex = 0;
            this.hitPointsLabel.Text = "Hit Points:";
            // 
            // goldLabel
            // 
            this.goldLabel.AutoSize = true;
            this.goldLabel.Location = new System.Drawing.Point(16, 29);
            this.goldLabel.Name = "goldLabel";
            this.goldLabel.Size = new System.Drawing.Size(32, 13);
            this.goldLabel.TabIndex = 1;
            this.goldLabel.Text = "Gold:";
            // 
            // experienceLabel
            // 
            this.experienceLabel.AutoSize = true;
            this.experienceLabel.Location = new System.Drawing.Point(16, 42);
            this.experienceLabel.Name = "experienceLabel";
            this.experienceLabel.Size = new System.Drawing.Size(63, 13);
            this.experienceLabel.TabIndex = 2;
            this.experienceLabel.Text = "Experience:";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(16, 55);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(36, 13);
            this.levelLabel.TabIndex = 3;
            this.levelLabel.Text = "Level:";
            // 
            // hitPointsValueLabel
            // 
            this.hitPointsValueLabel.AutoSize = true;
            this.hitPointsValueLabel.Location = new System.Drawing.Point(85, 16);
            this.hitPointsValueLabel.Name = "hitPointsValueLabel";
            this.hitPointsValueLabel.Size = new System.Drawing.Size(13, 13);
            this.hitPointsValueLabel.TabIndex = 4;
            this.hitPointsValueLabel.Text = "0";
            // 
            // goldValueLabel
            // 
            this.goldValueLabel.AutoSize = true;
            this.goldValueLabel.Location = new System.Drawing.Point(85, 29);
            this.goldValueLabel.Name = "goldValueLabel";
            this.goldValueLabel.Size = new System.Drawing.Size(13, 13);
            this.goldValueLabel.TabIndex = 5;
            this.goldValueLabel.Text = "0";
            // 
            // experienceValueLabel
            // 
            this.experienceValueLabel.AutoSize = true;
            this.experienceValueLabel.Location = new System.Drawing.Point(85, 42);
            this.experienceValueLabel.Name = "experienceValueLabel";
            this.experienceValueLabel.Size = new System.Drawing.Size(13, 13);
            this.experienceValueLabel.TabIndex = 6;
            this.experienceValueLabel.Text = "0";
            // 
            // levelValueLabel
            // 
            this.levelValueLabel.AutoSize = true;
            this.levelValueLabel.Location = new System.Drawing.Point(85, 55);
            this.levelValueLabel.Name = "levelValueLabel";
            this.levelValueLabel.Size = new System.Drawing.Size(13, 13);
            this.levelValueLabel.TabIndex = 7;
            this.levelValueLabel.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 651);
            this.Controls.Add(this.levelValueLabel);
            this.Controls.Add(this.experienceValueLabel);
            this.Controls.Add(this.goldValueLabel);
            this.Controls.Add(this.hitPointsValueLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.experienceLabel);
            this.Controls.Add(this.goldLabel);
            this.Controls.Add(this.hitPointsLabel);
            this.Name = "MainForm";
            this.Text = "Adventure";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hitPointsLabel;
        private System.Windows.Forms.Label goldLabel;
        private System.Windows.Forms.Label experienceLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label hitPointsValueLabel;
        private System.Windows.Forms.Label goldValueLabel;
        private System.Windows.Forms.Label experienceValueLabel;
        private System.Windows.Forms.Label levelValueLabel;
    }
}

