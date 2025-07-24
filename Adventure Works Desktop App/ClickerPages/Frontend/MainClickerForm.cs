using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ClickerPages.Frontend
{
    public partial class MainClickerForm : Form
    {
        private int userPower = 1;
        public MainClickerForm(string username)
        {
            InitializeComponent();
        }

        private void clickMeButton_Click(object sender, EventArgs e)
        {
            int currNum = int.Parse(numOfClicksLabel.Text);

            currNum += userPower;

            numOfClicksLabel.Text = currNum.ToString();
            CenterClickNumber();
        }

        private void CenterClickNumber()
        {
            int x = (numOfClicksPanels.Size.Width - numOfClicksLabel.Size.Width) / 2;
            int y = (numOfClicksPanels.Size.Height - numOfClicksLabel.Size.Height) / 2;
            numOfClicksLabel.Location = new Point(x,y);
        }
    }
}
