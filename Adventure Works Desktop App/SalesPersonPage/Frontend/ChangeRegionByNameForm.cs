using Adventure_Works_Desktop_App.SalesPersonPage.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    public partial class ChangeRegionByNameForm : Form
    {
        public bool submitPressed = false;
        private SalesPersonBackend backend = new SalesPersonBackend();
        public string regionName;
        public ChangeRegionByNameForm()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (backend.RegionIsValid(regionNameTextBox.Text))
            {
                regionName = regionNameTextBox.Text;
                submitPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Region\nPlease try again.");
            }
        }
    }
}
