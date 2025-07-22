using Adventure_Works_Desktop_App.SalesPersonPage.Backend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    public partial class ChangeRegionByNameForm : Form
    {
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
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid Region\nPlease try again.");
            }
        }
    }
}
