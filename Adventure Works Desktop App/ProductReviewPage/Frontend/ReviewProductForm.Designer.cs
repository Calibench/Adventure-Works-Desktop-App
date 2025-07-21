namespace Adventure_Works_Desktop_App.ProductReviewPage.Frontend
{
    partial class ReviewProductForm
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
            this.backButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.productReviewGroupBox = new System.Windows.Forms.GroupBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.reviewPanel = new System.Windows.Forms.Panel();
            this.reviewLabel = new System.Windows.Forms.Label();
            this.customerReviewRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ratingPanel = new System.Windows.Forms.Panel();
            this.ratingLabel = new System.Windows.Forms.Label();
            this.oneStarRadioButton = new System.Windows.Forms.RadioButton();
            this.twoStarRadioButton = new System.Windows.Forms.RadioButton();
            this.fiveStarRadioButton = new System.Windows.Forms.RadioButton();
            this.threeStarRadioButton = new System.Windows.Forms.RadioButton();
            this.fourStarRadioButton = new System.Windows.Forms.RadioButton();
            this.namePanel = new System.Windows.Forms.Panel();
            this.productNameLabel = new System.Windows.Forms.Label();
            this.productNameValueLabel = new System.Windows.Forms.Label();
            this.productIDLabel = new System.Windows.Forms.Label();
            this.productIDValueLabel = new System.Windows.Forms.Label();
            this.usernamePanel = new System.Windows.Forms.Panel();
            this.productReviewGroupBox.SuspendLayout();
            this.reviewPanel.SuspendLayout();
            this.ratingPanel.SuspendLayout();
            this.namePanel.SuspendLayout();
            this.usernamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.backButton.Location = new System.Drawing.Point(85, 21);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.usernameLabel.Location = new System.Drawing.Point(72, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Padding = new System.Windows.Forms.Padding(0, 5, 10, 0);
            this.usernameLabel.Size = new System.Drawing.Size(100, 18);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "Logged in: USER";
            // 
            // productReviewGroupBox
            // 
            this.productReviewGroupBox.Controls.Add(this.submitButton);
            this.productReviewGroupBox.Controls.Add(this.reviewPanel);
            this.productReviewGroupBox.Controls.Add(this.ratingPanel);
            this.productReviewGroupBox.Controls.Add(this.namePanel);
            this.productReviewGroupBox.Controls.Add(this.productIDLabel);
            this.productReviewGroupBox.Controls.Add(this.productIDValueLabel);
            this.productReviewGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productReviewGroupBox.Location = new System.Drawing.Point(12, 12);
            this.productReviewGroupBox.Name = "productReviewGroupBox";
            this.productReviewGroupBox.Size = new System.Drawing.Size(543, 315);
            this.productReviewGroupBox.TabIndex = 0;
            this.productReviewGroupBox.TabStop = false;
            this.productReviewGroupBox.Text = "Product Review";
            // 
            // submitButton
            // 
            this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(234, 288);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 4;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.SubmitButtonClicked);
            // 
            // reviewPanel
            // 
            this.reviewPanel.Controls.Add(this.reviewLabel);
            this.reviewPanel.Controls.Add(this.customerReviewRichTextBox);
            this.reviewPanel.Location = new System.Drawing.Point(51, 109);
            this.reviewPanel.Name = "reviewPanel";
            this.reviewPanel.Size = new System.Drawing.Size(440, 176);
            this.reviewPanel.TabIndex = 3;
            // 
            // reviewLabel
            // 
            this.reviewLabel.AutoSize = true;
            this.reviewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reviewLabel.Location = new System.Drawing.Point(3, 4);
            this.reviewLabel.Name = "reviewLabel";
            this.reviewLabel.Size = new System.Drawing.Size(46, 13);
            this.reviewLabel.TabIndex = 10;
            this.reviewLabel.Text = "Review:";
            // 
            // customerReviewRichTextBox
            // 
            this.customerReviewRichTextBox.Location = new System.Drawing.Point(6, 20);
            this.customerReviewRichTextBox.Name = "customerReviewRichTextBox";
            this.customerReviewRichTextBox.Size = new System.Drawing.Size(431, 145);
            this.customerReviewRichTextBox.TabIndex = 0;
            this.customerReviewRichTextBox.Text = "";
            // 
            // ratingPanel
            // 
            this.ratingPanel.Controls.Add(this.ratingLabel);
            this.ratingPanel.Controls.Add(this.oneStarRadioButton);
            this.ratingPanel.Controls.Add(this.twoStarRadioButton);
            this.ratingPanel.Controls.Add(this.fiveStarRadioButton);
            this.ratingPanel.Controls.Add(this.threeStarRadioButton);
            this.ratingPanel.Controls.Add(this.fourStarRadioButton);
            this.ratingPanel.Location = new System.Drawing.Point(51, 60);
            this.ratingPanel.Name = "ratingPanel";
            this.ratingPanel.Size = new System.Drawing.Size(440, 46);
            this.ratingPanel.TabIndex = 2;
            // 
            // ratingLabel
            // 
            this.ratingLabel.AutoSize = true;
            this.ratingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratingLabel.Location = new System.Drawing.Point(3, 5);
            this.ratingLabel.Name = "ratingLabel";
            this.ratingLabel.Size = new System.Drawing.Size(41, 13);
            this.ratingLabel.TabIndex = 5;
            this.ratingLabel.Text = "Rating:";
            // 
            // oneStarRadioButton
            // 
            this.oneStarRadioButton.AutoSize = true;
            this.oneStarRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oneStarRadioButton.Location = new System.Drawing.Point(6, 21);
            this.oneStarRadioButton.Name = "oneStarRadioButton";
            this.oneStarRadioButton.Size = new System.Drawing.Size(67, 17);
            this.oneStarRadioButton.TabIndex = 0;
            this.oneStarRadioButton.TabStop = true;
            this.oneStarRadioButton.Tag = "oneStar";
            this.oneStarRadioButton.Text = "One Star";
            this.oneStarRadioButton.UseVisualStyleBackColor = true;
            // 
            // twoStarRadioButton
            // 
            this.twoStarRadioButton.AutoSize = true;
            this.twoStarRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twoStarRadioButton.Location = new System.Drawing.Point(97, 21);
            this.twoStarRadioButton.Name = "twoStarRadioButton";
            this.twoStarRadioButton.Size = new System.Drawing.Size(68, 17);
            this.twoStarRadioButton.TabIndex = 1;
            this.twoStarRadioButton.TabStop = true;
            this.twoStarRadioButton.Tag = "twoStar";
            this.twoStarRadioButton.Text = "Two Star";
            this.twoStarRadioButton.UseVisualStyleBackColor = true;
            // 
            // fiveStarRadioButton
            // 
            this.fiveStarRadioButton.AutoSize = true;
            this.fiveStarRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fiveStarRadioButton.Location = new System.Drawing.Point(370, 21);
            this.fiveStarRadioButton.Name = "fiveStarRadioButton";
            this.fiveStarRadioButton.Size = new System.Drawing.Size(67, 17);
            this.fiveStarRadioButton.TabIndex = 4;
            this.fiveStarRadioButton.TabStop = true;
            this.fiveStarRadioButton.Tag = "fiveStar";
            this.fiveStarRadioButton.Text = "Five Star";
            this.fiveStarRadioButton.UseVisualStyleBackColor = true;
            // 
            // threeStarRadioButton
            // 
            this.threeStarRadioButton.AutoSize = true;
            this.threeStarRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.threeStarRadioButton.Location = new System.Drawing.Point(188, 21);
            this.threeStarRadioButton.Name = "threeStarRadioButton";
            this.threeStarRadioButton.Size = new System.Drawing.Size(75, 17);
            this.threeStarRadioButton.TabIndex = 2;
            this.threeStarRadioButton.TabStop = true;
            this.threeStarRadioButton.Tag = "threeStar";
            this.threeStarRadioButton.Text = "Three Star";
            this.threeStarRadioButton.UseVisualStyleBackColor = true;
            // 
            // fourStarRadioButton
            // 
            this.fourStarRadioButton.AutoSize = true;
            this.fourStarRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourStarRadioButton.Location = new System.Drawing.Point(279, 21);
            this.fourStarRadioButton.Name = "fourStarRadioButton";
            this.fourStarRadioButton.Size = new System.Drawing.Size(68, 17);
            this.fourStarRadioButton.TabIndex = 3;
            this.fourStarRadioButton.TabStop = true;
            this.fourStarRadioButton.Tag = "fourStar";
            this.fourStarRadioButton.Text = "Four Star";
            this.fourStarRadioButton.UseVisualStyleBackColor = true;
            // 
            // namePanel
            // 
            this.namePanel.Controls.Add(this.productNameLabel);
            this.namePanel.Controls.Add(this.productNameValueLabel);
            this.namePanel.Location = new System.Drawing.Point(51, 35);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(440, 22);
            this.namePanel.TabIndex = 0;
            // 
            // productNameLabel
            // 
            this.productNameLabel.AutoSize = true;
            this.productNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productNameLabel.Location = new System.Drawing.Point(3, 0);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Size = new System.Drawing.Size(78, 13);
            this.productNameLabel.TabIndex = 3;
            this.productNameLabel.Text = "Product Name:";
            // 
            // productNameValueLabel
            // 
            this.productNameValueLabel.AutoSize = true;
            this.productNameValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productNameValueLabel.Location = new System.Drawing.Point(87, 0);
            this.productNameValueLabel.Name = "productNameValueLabel";
            this.productNameValueLabel.Size = new System.Drawing.Size(103, 13);
            this.productNameValueLabel.TabIndex = 0;
            this.productNameValueLabel.Text = "Select Product Here";
            this.productNameValueLabel.Click += new System.EventHandler(this.ProductNameValueLabelClicked);
            this.productNameValueLabel.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.productNameValueLabel.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // productIDLabel
            // 
            this.productIDLabel.AutoSize = true;
            this.productIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productIDLabel.Location = new System.Drawing.Point(481, 18);
            this.productIDLabel.Name = "productIDLabel";
            this.productIDLabel.Size = new System.Drawing.Size(21, 13);
            this.productIDLabel.TabIndex = 1;
            this.productIDLabel.Text = "ID:";
            // 
            // productIDValueLabel
            // 
            this.productIDValueLabel.AutoSize = true;
            this.productIDValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productIDValueLabel.Location = new System.Drawing.Point(508, 18);
            this.productIDValueLabel.Name = "productIDValueLabel";
            this.productIDValueLabel.Size = new System.Drawing.Size(25, 13);
            this.productIDValueLabel.TabIndex = 1;
            this.productIDValueLabel.Text = "621";
            this.productIDValueLabel.Click += new System.EventHandler(this.ProductNameValueLabelClicked);
            this.productIDValueLabel.MouseEnter += new System.EventHandler(this.MouseEnter);
            this.productIDValueLabel.MouseLeave += new System.EventHandler(this.MouseLeave);
            // 
            // usernamePanel
            // 
            this.usernamePanel.Controls.Add(this.usernameLabel);
            this.usernamePanel.Controls.Add(this.backButton);
            this.usernamePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.usernamePanel.Location = new System.Drawing.Point(560, 0);
            this.usernamePanel.Name = "usernamePanel";
            this.usernamePanel.Size = new System.Drawing.Size(172, 332);
            this.usernamePanel.TabIndex = 2;
            // 
            // ReviewProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 332);
            this.Controls.Add(this.usernamePanel);
            this.Controls.Add(this.productReviewGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReviewProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Review Product";
            this.Load += new System.EventHandler(this.InitialFormLoad);
            this.productReviewGroupBox.ResumeLayout(false);
            this.productReviewGroupBox.PerformLayout();
            this.reviewPanel.ResumeLayout(false);
            this.reviewPanel.PerformLayout();
            this.ratingPanel.ResumeLayout(false);
            this.ratingPanel.PerformLayout();
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.usernamePanel.ResumeLayout(false);
            this.usernamePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.GroupBox productReviewGroupBox;
        private System.Windows.Forms.Label productIDLabel;
        private System.Windows.Forms.Label productIDValueLabel;
        private System.Windows.Forms.Label productNameValueLabel;
        private System.Windows.Forms.Label productNameLabel;
        private System.Windows.Forms.Label ratingLabel;
        private System.Windows.Forms.RadioButton fiveStarRadioButton;
        private System.Windows.Forms.RadioButton fourStarRadioButton;
        private System.Windows.Forms.RadioButton threeStarRadioButton;
        private System.Windows.Forms.RadioButton twoStarRadioButton;
        private System.Windows.Forms.RadioButton oneStarRadioButton;
        private System.Windows.Forms.Label reviewLabel;
        private System.Windows.Forms.RichTextBox customerReviewRichTextBox;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.Panel reviewPanel;
        private System.Windows.Forms.Panel ratingPanel;
        private System.Windows.Forms.Panel usernamePanel;
        private System.Windows.Forms.Button submitButton;
    }
}