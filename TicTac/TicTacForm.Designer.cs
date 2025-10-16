using System.Drawing;
using System.Windows.Forms;

namespace TicTac
{
    partial class TicTacForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            btnGrid1 = new Button();
            groupBox2 = new GroupBox();
            btnGrid2 = new Button();
            groupBox3 = new GroupBox();
            btnGrid3 = new Button();
            groupBox4 = new GroupBox();
            btnGrid4 = new Button();
            groupBox5 = new GroupBox();
            btnGrid5 = new Button();
            groupBox6 = new GroupBox();
            btnGrid6 = new Button();
            groupBox7 = new GroupBox();
            btnGrid7 = new Button();
            groupBox8 = new GroupBox();
            btnGrid8 = new Button();
            groupBox9 = new GroupBox();
            btnGrid9 = new Button();
            btnRestart = new Button();
            lblScoreTitle = new Label();
            lblPlayerScore = new Label();
            lblBotScore = new Label();
            lblWinner = new Label();
            lblTurn = new Label();
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Controls.Add(groupBox3);
            flowLayoutPanel1.Controls.Add(groupBox4);
            flowLayoutPanel1.Controls.Add(groupBox5);
            flowLayoutPanel1.Controls.Add(groupBox6);
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Controls.Add(groupBox8);
            flowLayoutPanel1.Controls.Add(groupBox9);
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(381, 386);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGrid1);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(120, 120);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnGrid1
            // 
            btnGrid1.Dock = DockStyle.Fill;
            btnGrid1.Location = new Point(3, 19);
            btnGrid1.Name = "btnGrid1";
            btnGrid1.Size = new Size(114, 98);
            btnGrid1.TabIndex = 0;
            btnGrid1.UseVisualStyleBackColor = true;
            btnGrid1.Click += btnGrid_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnGrid2);
            groupBox2.Location = new Point(129, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(120, 120);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // btnGrid2
            // 
            btnGrid2.Dock = DockStyle.Fill;
            btnGrid2.Location = new Point(3, 19);
            btnGrid2.Name = "btnGrid2";
            btnGrid2.Size = new Size(114, 98);
            btnGrid2.TabIndex = 0;
            btnGrid2.UseVisualStyleBackColor = true;
            btnGrid2.Click += btnGrid_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnGrid3);
            groupBox3.Location = new Point(255, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(120, 120);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            // 
            // btnGrid3
            // 
            btnGrid3.Dock = DockStyle.Fill;
            btnGrid3.Location = new Point(3, 19);
            btnGrid3.Name = "btnGrid3";
            btnGrid3.Size = new Size(114, 98);
            btnGrid3.TabIndex = 0;
            btnGrid3.UseVisualStyleBackColor = true;
            btnGrid3.Click += btnGrid_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnGrid4);
            groupBox4.Location = new Point(3, 129);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(120, 120);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            // 
            // btnGrid4
            // 
            btnGrid4.Dock = DockStyle.Fill;
            btnGrid4.Location = new Point(3, 19);
            btnGrid4.Name = "btnGrid4";
            btnGrid4.Size = new Size(114, 98);
            btnGrid4.TabIndex = 0;
            btnGrid4.UseVisualStyleBackColor = true;
            btnGrid4.Click += btnGrid_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnGrid5);
            groupBox5.Location = new Point(129, 129);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(120, 120);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            // 
            // btnGrid5
            // 
            btnGrid5.Dock = DockStyle.Fill;
            btnGrid5.Location = new Point(3, 19);
            btnGrid5.Name = "btnGrid5";
            btnGrid5.Size = new Size(114, 98);
            btnGrid5.TabIndex = 0;
            btnGrid5.UseVisualStyleBackColor = true;
            btnGrid5.Click += btnGrid_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btnGrid6);
            groupBox6.Location = new Point(255, 129);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(120, 120);
            groupBox6.TabIndex = 2;
            groupBox6.TabStop = false;
            // 
            // btnGrid6
            // 
            btnGrid6.Dock = DockStyle.Fill;
            btnGrid6.Location = new Point(3, 19);
            btnGrid6.Name = "btnGrid6";
            btnGrid6.Size = new Size(114, 98);
            btnGrid6.TabIndex = 0;
            btnGrid6.UseVisualStyleBackColor = true;
            btnGrid6.Click += btnGrid_Click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(btnGrid7);
            groupBox7.Location = new Point(3, 255);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(120, 120);
            groupBox7.TabIndex = 2;
            groupBox7.TabStop = false;
            // 
            // btnGrid7
            // 
            btnGrid7.Dock = DockStyle.Fill;
            btnGrid7.Location = new Point(3, 19);
            btnGrid7.Name = "btnGrid7";
            btnGrid7.Size = new Size(114, 98);
            btnGrid7.TabIndex = 0;
            btnGrid7.UseVisualStyleBackColor = true;
            btnGrid7.Click += btnGrid_Click;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnGrid8);
            groupBox8.Location = new Point(129, 255);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(120, 120);
            groupBox8.TabIndex = 2;
            groupBox8.TabStop = false;
            // 
            // btnGrid8
            // 
            btnGrid8.Dock = DockStyle.Fill;
            btnGrid8.Location = new Point(3, 19);
            btnGrid8.Name = "btnGrid8";
            btnGrid8.Size = new Size(114, 98);
            btnGrid8.TabIndex = 0;
            btnGrid8.UseVisualStyleBackColor = true;
            btnGrid8.Click += btnGrid_Click;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(btnGrid9);
            groupBox9.Location = new Point(255, 255);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(120, 120);
            groupBox9.TabIndex = 2;
            groupBox9.TabStop = false;
            // 
            // btnGrid9
            // 
            btnGrid9.Dock = DockStyle.Fill;
            btnGrid9.Location = new Point(3, 19);
            btnGrid9.Name = "btnGrid9";
            btnGrid9.Size = new Size(114, 98);
            btnGrid9.TabIndex = 0;
            btnGrid9.UseVisualStyleBackColor = true;
            btnGrid9.Click += btnGrid_Click;
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(411, 15);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(75, 23);
            btnRestart.TabIndex = 1;
            btnRestart.Text = "RESTART";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // lblScoreTitle
            // 
            lblScoreTitle.AutoSize = true;
            lblScoreTitle.Location = new Point(424, 41);
            lblScoreTitle.Name = "lblScoreTitle";
            lblScoreTitle.Size = new Size(49, 15);
            lblScoreTitle.TabIndex = 2;
            lblScoreTitle.Text = "SCORES";
            // 
            // lblPlayerScore
            // 
            lblPlayerScore.AutoSize = true;
            lblPlayerScore.Location = new Point(407, 56);
            lblPlayerScore.Name = "lblPlayerScore";
            lblPlayerScore.Size = new Size(83, 15);
            lblPlayerScore.TabIndex = 3;
            lblPlayerScore.Text = "Player Score: 0";
            // 
            // lblBotScore
            // 
            lblBotScore.Anchor = AnchorStyles.Left;
            lblBotScore.AutoSize = true;
            lblBotScore.Location = new Point(407, 71);
            lblBotScore.Name = "lblBotScore";
            lblBotScore.Size = new Size(73, 15);
            lblBotScore.TabIndex = 4;
            lblBotScore.Text = "Computer: 0";
            // 
            // lblWinner
            // 
            lblWinner.AutoSize = true;
            lblWinner.Location = new Point(409, 369);
            lblWinner.Name = "lblWinner";
            lblWinner.Size = new Size(79, 15);
            lblWinner.TabIndex = 5;
            lblWinner.Text = "PLAYER WINS";
            lblWinner.Visible = false;
            // 
            // lblTurn
            // 
            lblTurn.AutoSize = true;
            lblTurn.Location = new Point(423, 354);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(50, 15);
            lblTurn.TabIndex = 6;
            lblTurn.Text = "X's Turn";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 409);
            Controls.Add(lblTurn);
            Controls.Add(lblWinner);
            Controls.Add(lblBotScore);
            Controls.Add(lblPlayerScore);
            Controls.Add(lblScoreTitle);
            Controls.Add(btnRestart);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            flowLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private Button btnGrid1;
        private Button btnGrid2;
        private Button btnGrid3;
        private Button btnGrid4;
        private Button btnGrid5;
        private Button btnGrid6;
        private Button btnGrid7;
        private Button btnGrid8;
        private Button btnGrid9;
        private Button btnRestart;
        private Label lblScoreTitle;
        private Label lblPlayerScore;
        private Label lblBotScore;
        private Label lblWinner;
        private Label lblTurn;
    }
}

