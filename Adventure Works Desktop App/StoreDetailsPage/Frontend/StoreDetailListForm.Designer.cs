namespace Adventure_Works_Desktop_App.StoreDetailsPage.Frontend
{
    partial class StoreDetailListForm
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
            this.components = new System.ComponentModel.Container();
            this.usernameBackPanel = new System.Windows.Forms.Panel();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.quickSearchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sortByListComboBox = new System.Windows.Forms.ComboBox();
            this.sortListByLabel = new System.Windows.Forms.Label();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.countLabel = new System.Windows.Forms.Label();
            this.storeListView = new System.Windows.Forms.ListView();
            this.businessIDColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.storeNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addresLine1ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.postalCodeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.countryColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yearColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.specialtyColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.businessTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usernameBackPanel.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.listViewContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameBackPanel
            // 
            this.usernameBackPanel.Controls.Add(this.usernameLabel);
            this.usernameBackPanel.Controls.Add(this.backButton);
            this.usernameBackPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.usernameBackPanel.Location = new System.Drawing.Point(684, 0);
            this.usernameBackPanel.Name = "usernameBackPanel";
            this.usernameBackPanel.Size = new System.Drawing.Size(154, 441);
            this.usernameBackPanel.TabIndex = 3;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.usernameLabel.Location = new System.Drawing.Point(58, 0);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Padding = new System.Windows.Forms.Padding(0, 5, 5, 0);
            this.usernameLabel.Size = new System.Drawing.Size(96, 18);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "Logged In: USER";
            // 
            // backButton
            // 
            this.backButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.backButton.Location = new System.Drawing.Point(76, 21);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.quickSearchTextBox);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.sortByListComboBox);
            this.topPanel.Controls.Add(this.sortListByLabel);
            this.topPanel.Controls.Add(this.countTextBox);
            this.topPanel.Controls.Add(this.countLabel);
            this.topPanel.Location = new System.Drawing.Point(12, 12);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(473, 32);
            this.topPanel.TabIndex = 2;
            // 
            // quickSearchTextBox
            // 
            this.quickSearchTextBox.Location = new System.Drawing.Point(365, 5);
            this.quickSearchTextBox.Name = "quickSearchTextBox";
            this.quickSearchTextBox.Size = new System.Drawing.Size(100, 20);
            this.quickSearchTextBox.TabIndex = 5;
            this.quickSearchTextBox.TextChanged += new System.EventHandler(this.quickSearchTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Quick Search:";
            // 
            // sortByListComboBox
            // 
            this.sortByListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortByListComboBox.FormattingEnabled = true;
            this.sortByListComboBox.Items.AddRange(new object[] {
            "Store Name:",
            "City:",
            "Country:",
            "Year:"});
            this.sortByListComboBox.Location = new System.Drawing.Point(175, 6);
            this.sortByListComboBox.Name = "sortByListComboBox";
            this.sortByListComboBox.Size = new System.Drawing.Size(103, 21);
            this.sortByListComboBox.TabIndex = 3;
            this.sortByListComboBox.SelectedIndexChanged += new System.EventHandler(this.sortByListComboBox_SelectedIndexChanged);
            // 
            // sortListByLabel
            // 
            this.sortListByLabel.AutoSize = true;
            this.sortListByLabel.Location = new System.Drawing.Point(106, 9);
            this.sortListByLabel.Name = "sortListByLabel";
            this.sortListByLabel.Size = new System.Drawing.Size(63, 13);
            this.sortListByLabel.TabIndex = 2;
            this.sortListByLabel.Text = "Sort List By:";
            // 
            // countTextBox
            // 
            this.countTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.countTextBox.Location = new System.Drawing.Point(47, 6);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.ReadOnly = true;
            this.countTextBox.Size = new System.Drawing.Size(53, 20);
            this.countTextBox.TabIndex = 1;
            this.countTextBox.Text = "99999999";
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(3, 9);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(38, 13);
            this.countLabel.TabIndex = 0;
            this.countLabel.Text = "Count:";
            // 
            // storeListView
            // 
            this.storeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.businessIDColumnHeader,
            this.storeNameColumnHeader,
            this.addresLine1ColumnHeader,
            this.cityColumnHeader,
            this.stateColumnHeader,
            this.postalCodeColumnHeader,
            this.countryColumnHeader,
            this.yearColumnHeader,
            this.specialtyColumnHeader,
            this.businessTypeColumnHeader});
            this.storeListView.FullRowSelect = true;
            this.storeListView.HideSelection = false;
            this.storeListView.LabelWrap = false;
            this.storeListView.Location = new System.Drawing.Point(12, 50);
            this.storeListView.MultiSelect = false;
            this.storeListView.Name = "storeListView";
            this.storeListView.Size = new System.Drawing.Size(666, 379);
            this.storeListView.TabIndex = 6;
            this.storeListView.UseCompatibleStateImageBehavior = false;
            this.storeListView.View = System.Windows.Forms.View.Details;
            this.storeListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.storeListView_ColumnClick);
            this.storeListView.DoubleClick += new System.EventHandler(this.storeListView_Click);
            this.storeListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContextWindowMouseDown);
            // 
            // businessIDColumnHeader
            // 
            this.businessIDColumnHeader.Text = "ID";
            this.businessIDColumnHeader.Width = 41;
            // 
            // storeNameColumnHeader
            // 
            this.storeNameColumnHeader.Text = "Store Name";
            this.storeNameColumnHeader.Width = 113;
            // 
            // addresLine1ColumnHeader
            // 
            this.addresLine1ColumnHeader.Text = "Address Line 1";
            this.addresLine1ColumnHeader.Width = 102;
            // 
            // cityColumnHeader
            // 
            this.cityColumnHeader.Text = "City";
            // 
            // stateColumnHeader
            // 
            this.stateColumnHeader.Text = "State";
            // 
            // postalCodeColumnHeader
            // 
            this.postalCodeColumnHeader.Text = "Postal Code";
            // 
            // countryColumnHeader
            // 
            this.countryColumnHeader.Text = "Country";
            // 
            // yearColumnHeader
            // 
            this.yearColumnHeader.Text = "Year";
            // 
            // specialtyColumnHeader
            // 
            this.specialtyColumnHeader.Text = "Specialty";
            // 
            // businessTypeColumnHeader
            // 
            this.businessTypeColumnHeader.Text = "Type";
            // 
            // listViewContextMenuStrip
            // 
            this.listViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editStoreToolStripMenuItem});
            this.listViewContextMenuStrip.Name = "listViewContextMenuStrip";
            this.listViewContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.listViewContextMenuStrip.ShowImageMargin = false;
            this.listViewContextMenuStrip.Size = new System.Drawing.Size(127, 26);
            // 
            // editStoreToolStripMenuItem
            // 
            this.editStoreToolStripMenuItem.Name = "editStoreToolStripMenuItem";
            this.editStoreToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.editStoreToolStripMenuItem.Text = "Edit Store Data";
            this.editStoreToolStripMenuItem.Click += new System.EventHandler(this.editStoreToolStripMenuItem_Click);
            // 
            // StoreDetailListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.storeListView);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.usernameBackPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StoreDetailListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "StoreDetailListForm";
            this.Load += new System.EventHandler(this.InitialFormLoad);
            this.usernameBackPanel.ResumeLayout(false);
            this.usernameBackPanel.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.listViewContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel usernameBackPanel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Label sortListByLabel;
        private System.Windows.Forms.TextBox countTextBox;
        private System.Windows.Forms.TextBox quickSearchTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sortByListComboBox;
        private System.Windows.Forms.ListView storeListView;
        private System.Windows.Forms.ColumnHeader businessIDColumnHeader;
        private System.Windows.Forms.ColumnHeader storeNameColumnHeader;
        private System.Windows.Forms.ColumnHeader addresLine1ColumnHeader;
        private System.Windows.Forms.ColumnHeader cityColumnHeader;
        private System.Windows.Forms.ColumnHeader stateColumnHeader;
        private System.Windows.Forms.ColumnHeader postalCodeColumnHeader;
        private System.Windows.Forms.ColumnHeader countryColumnHeader;
        private System.Windows.Forms.ColumnHeader yearColumnHeader;
        private System.Windows.Forms.ColumnHeader specialtyColumnHeader;
        private System.Windows.Forms.ColumnHeader businessTypeColumnHeader;
        private System.Windows.Forms.ContextMenuStrip listViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editStoreToolStripMenuItem;
    }
}