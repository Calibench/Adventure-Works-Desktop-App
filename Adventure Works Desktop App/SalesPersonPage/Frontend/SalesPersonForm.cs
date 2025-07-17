using Adventure_Works_Desktop_App.SalesPersonPage.Backend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Frontend
{
    public partial class SalesPersonForm : Form
    {
        public bool backButtonPressed = false;
        private string username;
        private SalesPersonBackend backend = new SalesPersonBackend();
        public SalesPersonForm(string username)
        {
            InitializeComponent();
            this.username = username;
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
            if (frm.submitPressed)
            {
                regionRegionNameValueLabel.Text = frm.regionName;
                ShowSpecificRegionData();
            }
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            usernameLabel.Text = $"Logged in: {username}";
            salesPersonGroupBox.Visible = false;
            regionSalesGroupBox.Visible = false;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ExitBackToMenu();
        }

        // helper methods
        private void ExitBackToMenu()
        {
            backButtonPressed = true;
            this.Close();
        }

        private void SelectSalesPerson(string formName, string title)
        {
            var frm = new ChangeSalesPersonByIDForm(formName, title);
            frm.Location = this.Location;
            frm.ShowDialog();

            if (frm.submitPressed)
            {
                idValueLabel.Text = frm.SelectedID;
                ShowSalesData();
            }
        }

        private void CenterTextGroupBox(GroupBox groupbox, Control control)
        {
            control.Left = (groupbox.Width / 2) - (control.Width / 2);
            control.Top = (groupbox.Height / 2) - (control.Height / 2);
        }

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
