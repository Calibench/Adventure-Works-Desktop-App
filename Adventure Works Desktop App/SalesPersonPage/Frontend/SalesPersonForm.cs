using Adventure_Works_Desktop_App.SalesPersonPage.Backend;
using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    public partial class SalesPersonForm : Form
    {
        private string username;
        private SalesPersonBackend backend = new SalesPersonBackend();
        
        public SalesPersonForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            loggedBackUserControl.ChangeDisplayName(username);
            salesPersonGroupBox.Visible = false;
            regionSalesGroupBox.Visible = false;
        }

        private void ChangeSalesPersonByID(object sender, EventArgs e)
        {
            string formName = "ID";
            string title = "Enter ID";
            SelectSalesPerson(formName, title);
        }

        private void ChangeSalesPersonByName(object sender, EventArgs e)
        {
            string formName = "Name";
            string title = "Enter first and last name";
            SelectSalesPerson(formName, title);
        }

        private void ShowSpecificRegion(object sender, EventArgs e)
        {
            // Get data through region_name
            var frm = new ChangeRegionByNameForm();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                regionRegionNameValueLabel.Text = frm.regionName;
                ShowSpecificRegionData();
            }
        }

        // helper methods
        private void ExitBackToMenu(object sender, EventArgs e)
        {
            // This is for the menuStrip exit method
            this.DialogResult = DialogResult.Abort;
        }

        private void SelectSalesPerson(string formName, string title)
        {
            var frm = new ChangeSalesPersonByIDForm(formName, title);
            frm.Location = this.Location;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                idValueLabel.Text = frm.SelectedID;
                ShowSalesData();
            }
        }

        /// <summary>
        /// Centers a specified control within a given group box.
        /// </summary>
        /// <remarks>This method adjusts the position of the control so that it is centered both
        /// horizontally and vertically within the specified group box.</remarks>
        /// <param name="groupbox">The <see cref="GroupBox"/> within which the control will be centered.</param>
        /// <param name="control">The <see cref="Control"/> to be centered inside the group box.</param>
        private void CenterTextGroupBox(GroupBox groupbox, Control control)
        {
            control.Left = (groupbox.Width / 2) - (control.Width / 2);
            control.Top = (groupbox.Height / 2) - (control.Height / 2);
        }

        /// <summary>
        /// Displays detailed sales data for a specific region.
        /// </summary>
        /// <remarks>This method retrieves and displays sales data for a region specified by the <see
        /// cref="regionRegionNameValueLabel"/>. It updates various UI elements with the retrieved data, including
        /// territory ID, continent, region code, and sales figures for the current and previous year. The method also
        /// adjusts the visibility of relevant UI components to focus on regional sales data.</remarks>
        private void ShowSpecificRegionData()
        {
            RegionData data = new RegionData();
            data = backend.GetRegionData(regionRegionNameValueLabel.Text);

            territoryIDValueLabel.Text = data.TerritoryID;
            regionContinentValueLabel.Text = data.Continent;
            regionCountryCodeValueLabel.Text = data.RegionCode;
            
            totalSalesYTDValueLabel.Text = data.TotalSalesYTD;
            CenterTextGroupBox(regionSalesYTDGroupBox, totalSalesYTDValueLabel);

            totalSalesYTDLastYearValueLabel.Text = data.TotalSalesLY;
            CenterTextGroupBox(regionSalesLastYearGroupBox, totalSalesYTDLastYearValueLabel);

            regionSalesGroupBox.Visible = true;
            salesPersonGroupBox.Visible = false;
        }

        /// <summary>
        /// Displays the sales data for a salesperson and their associated region.
        /// </summary>
        /// <remarks>This method retrieves sales data for a specific salesperson using their ID and
        /// updates the UI with the relevant information, including personal details, sales performance, and regional
        /// sales data. It also adjusts the visibility of certain UI elements based on the presence of sales quota
        /// data.</remarks>
        private void ShowSalesData()
        {
            // with id assign data to value labels
            SalesPersonData data = new SalesPersonData();
            data = backend.GetSalesPersonData(idValueLabel.Text);

            // Sales Person
            firstNameValueLabel.Text = data.FirstName;
            lastNameValueLabel.Text = data.LastName;
            regionValueLabel.Text = data.RegionData.RegionName;
            countryCodeValueLabel.Text = data.RegionData.RegionCode;
            continentValueLabel.Text = data.RegionData.Continent;
            salesQuotaValueLabel.Text = data.SalesQuota;
            bonusValueLabel.Text = data.Bonus;

            string tempCommisionPCt = $"{(float.Parse(data.CommissionPct) * 100)}";
            commisionPctValueLabel.Text = tempCommisionPCt + "%";

            salesYTDValueLabel.Text = data.SalesYTD;
            CenterTextGroupBox(salesYTDGroupBox, salesYTDValueLabel);

            salesLastYearValueLabel.Text = data.SalesLY;
            CenterTextGroupBox(salesLastYearGroupBox, salesLastYearValueLabel);


            // Region Sales
            territoryIDValueLabel.Text = data.RegionData.TerritoryID;
            regionContinentValueLabel.Text = data.RegionData.Continent;
            regionCountryCodeValueLabel.Text = data.RegionData.RegionCode;
            regionRegionNameValueLabel.Text = data.RegionData.RegionName;

            totalSalesYTDValueLabel.Text = data.RegionData.TotalSalesYTD;
            CenterTextGroupBox(regionSalesYTDGroupBox, totalSalesYTDValueLabel);

            totalSalesYTDLastYearValueLabel.Text = data.RegionData.TotalSalesLY;
            CenterTextGroupBox(regionSalesLastYearGroupBox, totalSalesYTDLastYearValueLabel);

            if (salesQuotaValueLabel.Text == "")
            {
                managerLabel.Visible = true;
                regionSalesGroupBox.Visible = false;
            }
            else
            {
                managerLabel.Visible = false;
            }

            // show groupboxes finally
            salesPersonGroupBox.Visible = true;
            regionSalesGroupBox.Visible = true;
        }
    }
}
