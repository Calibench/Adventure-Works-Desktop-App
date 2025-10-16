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
            this.lblPower = new System.Windows.Forms.Label();
            this.upgradesPanel = new System.Windows.Forms.Panel();
            this.lblClickPowerLevel = new System.Windows.Forms.Label();
            this.lblClickPowerCost = new System.Windows.Forms.Label();
            this.btnBuyClickPower = new System.Windows.Forms.Button();
            this.lblAutoClickerLevel = new System.Windows.Forms.Label();
            this.lblAutoClickerCost = new System.Windows.Forms.Label();
            this.btnBuyAutoClicker = new System.Windows.Forms.Button();
            this.flpUpgrades = new System.Windows.Forms.FlowLayoutPanel();
            this.numOfClicksPanels.SuspendLayout();
            this.panel2.SuspendLayout();
            this.upgradesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clickMeButton
            // 
            this.clickMeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.clickMeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.clickMeButton.FlatAppearance.BorderSize = 0;
            this.clickMeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clickMeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.clickMeButton.ForeColor = System.Drawing.Color.White;
            this.clickMeButton.Location = new System.Drawing.Point(400, 20);
            this.clickMeButton.Name = "clickMeButton";
            this.clickMeButton.Size = new System.Drawing.Size(220, 80);
            this.clickMeButton.TabIndex = 0;
            this.clickMeButton.Text = "CLICK";
            this.clickMeButton.UseVisualStyleBackColor = false;
            this.clickMeButton.Click += new System.EventHandler(this.clickMeButton_Click);
            // 
            // numOfClicksLabel
            // 
            this.numOfClicksLabel.AutoSize = true;
            this.numOfClicksLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.numOfClicksLabel.ForeColor = System.Drawing.Color.Gold;
            this.numOfClicksLabel.Location = new System.Drawing.Point(91, 6);
            this.numOfClicksLabel.Name = "numOfClicksLabel";
            this.numOfClicksLabel.Size = new System.Drawing.Size(0, 32);
            this.numOfClicksLabel.TabIndex = 1;
            // 
            // numOfClicksPanels
            // 
            this.numOfClicksPanels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numOfClicksPanels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.numOfClicksPanels.Controls.Add(this.numOfClicksLabel);
            this.numOfClicksPanels.Location = new System.Drawing.Point(10, 10);
            this.numOfClicksPanels.Name = "numOfClicksPanels";
            this.numOfClicksPanels.Size = new System.Drawing.Size(360, 48);
            this.numOfClicksPanels.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel2.Controls.Add(this.lblPower);
            this.panel2.Controls.Add(this.numOfClicksPanels);
            this.panel2.Controls.Add(this.clickMeButton);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 120);
            this.panel2.TabIndex = 3;
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPower.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblPower.Location = new System.Drawing.Point(14, 70);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(68, 19);
            this.lblPower.TabIndex = 3;
            this.lblPower.Text = "Power: 1";
            // 
            // upgradesPanel
            // 
            this.upgradesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upgradesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));
            this.upgradesPanel.Controls.Add(this.flpUpgrades);
            this.upgradesPanel.Controls.Add(this.lblClickPowerLevel);
            this.upgradesPanel.Controls.Add(this.lblClickPowerCost);
            this.upgradesPanel.Controls.Add(this.btnBuyClickPower);
            this.upgradesPanel.Controls.Add(this.lblAutoClickerLevel);
            this.upgradesPanel.Controls.Add(this.lblAutoClickerCost);
            this.upgradesPanel.Controls.Add(this.btnBuyAutoClicker);
            this.upgradesPanel.Location = new System.Drawing.Point(12, 140);
            this.upgradesPanel.Name = "upgradesPanel";
            this.upgradesPanel.Size = new System.Drawing.Size(814, 289);
            this.upgradesPanel.TabIndex = 4;
            // 
            // lblClickPowerLevel
            // 
            this.lblClickPowerLevel.AutoSize = true;
            this.lblClickPowerLevel.Location = new System.Drawing.Point(15, 12);
            this.lblClickPowerLevel.Name = "lblClickPowerLevel";
            this.lblClickPowerLevel.Size = new System.Drawing.Size(108, 15);
            this.lblClickPowerLevel.TabIndex = 0;
            this.lblClickPowerLevel.Text = "Click Power Lvl: 0 (+1)";
            this.lblClickPowerLevel.Visible = false;
            // 
            // lblClickPowerCost
            // 
            this.lblClickPowerCost.AutoSize = true;
            this.lblClickPowerCost.Location = new System.Drawing.Point(15, 30);
            this.lblClickPowerCost.Name = "lblClickPowerCost";
            this.lblClickPowerCost.Size = new System.Drawing.Size(56, 15);
            this.lblClickPowerCost.TabIndex = 1;
            this.lblClickPowerCost.Text = "Cost: 10";
            this.lblClickPowerCost.Visible = false;
            // 
            // btnBuyClickPower
            // 
            this.btnBuyClickPower.Location = new System.Drawing.Point(200, 12);
            this.btnBuyClickPower.Name = "btnBuyClickPower";
            this.btnBuyClickPower.Size = new System.Drawing.Size(175, 31);
            this.btnBuyClickPower.TabIndex = 2;
            this.btnBuyClickPower.Text = "Buy Click Power";
            this.btnBuyClickPower.UseVisualStyleBackColor = true;
            this.btnBuyClickPower.Visible = false;
            this.btnBuyClickPower.Click += new System.EventHandler(this.btnBuyClickPower_Click);
            // 
            // lblAutoClickerLevel
            // 
            this.lblAutoClickerLevel.AutoSize = true;
            this.lblAutoClickerLevel.Location = new System.Drawing.Point(15, 68);
            this.lblAutoClickerLevel.Name = "lblAutoClickerLevel";
            this.lblAutoClickerLevel.Size = new System.Drawing.Size(126, 15);
            this.lblAutoClickerLevel.TabIndex = 3;
            this.lblAutoClickerLevel.Text = "Auto Clicker Lvl: 0 (+1/s)";
            this.lblAutoClickerLevel.Visible = false;
            // 
            // lblAutoClickerCost
            // 
            this.lblAutoClickerCost.AutoSize = true;
            this.lblAutoClickerCost.Location = new System.Drawing.Point(15, 86);
            this.lblAutoClickerCost.Name = "lblAutoClickerCost";
            this.lblAutoClickerCost.Size = new System.Drawing.Size(61, 15);
            this.lblAutoClickerCost.TabIndex = 4;
            this.lblAutoClickerCost.Text = "Cost: 50";
            this.lblAutoClickerCost.Visible = false;
            // 
            // btnBuyAutoClicker
            // 
            this.btnBuyAutoClicker.Location = new System.Drawing.Point(200, 68);
            this.btnBuyAutoClicker.Name = "btnBuyAutoClicker";
            this.btnBuyAutoClicker.Size = new System.Drawing.Size(175, 31);
            this.btnBuyAutoClicker.TabIndex = 5;
            this.btnBuyAutoClicker.Text = "Buy Auto Clicker";
            this.btnBuyAutoClicker.UseVisualStyleBackColor = true;
            this.btnBuyAutoClicker.Visible = false;
            this.btnBuyAutoClicker.Click += new System.EventHandler(this.btnBuyAutoClicker_Click);
            // 
            // flpUpgrades
            // 
            this.flpUpgrades.AutoScroll = true;
            this.flpUpgrades.BackColor = System.Drawing.Color.Transparent;
            this.flpUpgrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpUpgrades.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpUpgrades.Location = new System.Drawing.Point(0, 0);
            this.flpUpgrades.Margin = new System.Windows.Forms.Padding(10);
            this.flpUpgrades.Name = "flpUpgrades";
            this.flpUpgrades.Padding = new System.Windows.Forms.Padding(10);
            this.flpUpgrades.Size = new System.Drawing.Size(814, 289);
            this.flpUpgrades.TabIndex = 6;
            this.flpUpgrades.WrapContents = false;
            // 
            // MainClickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.upgradesPanel);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainClickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clicker";
            this.numOfClicksPanels.ResumeLayout(false);
            this.numOfClicksPanels.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.upgradesPanel.ResumeLayout(false);
            this.upgradesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clickMeButton;
        private System.Windows.Forms.Label numOfClicksLabel;
        private System.Windows.Forms.Panel numOfClicksPanels;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Panel upgradesPanel;
        private System.Windows.Forms.Label lblClickPowerLevel;
        private System.Windows.Forms.Label lblClickPowerCost;
        private System.Windows.Forms.Button btnBuyClickPower;
        private System.Windows.Forms.Label lblAutoClickerLevel;
        private System.Windows.Forms.Label lblAutoClickerCost;
        private System.Windows.Forms.Button btnBuyAutoClicker;
        private System.Windows.Forms.FlowLayoutPanel flpUpgrades;
    }
}