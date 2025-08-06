using Clicker.ClickerPages.Backend;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace Clicker.ClickerPages.Frontend
{
    public partial class MainClickerForm : Form
    {
        Player player = new Player();

        public MainClickerForm(string username)
        {
            InitializeComponent();
            CenterClickNumber();
        }

        private void clickMeButton_Click(object sender, EventArgs e)
        {
            NumberHandler numberHandler = new NumberHandler();
            player.Currency += player.UserPower;

            numOfClicksLabel.Text = numberHandler.FormatLargeNumber((double)player.Currency);
            CenterClickNumber();
        }

        private void CenterClickNumber()
        {
            int x = (numOfClicksPanels.Size.Width - numOfClicksLabel.Size.Width) / 2;
            int y = (numOfClicksPanels.Size.Height - numOfClicksLabel.Size.Height) / 2;
            numOfClicksLabel.Location = new Point(x, y);
        }
    }
}
