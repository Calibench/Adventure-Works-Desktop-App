using Adventure_Works_Desktop_App.Globals;
using Adventure_Works_Desktop_App.EmployeePage.Frontend;
using Adventure_Works_Desktop_App.ProductReviewPage.Frontend;
using Adventure_Works_Desktop_App.SalesPersonPage.Frontend;
using Adventure_Works_Desktop_App.StoreDetailsPage.Frontend;
using Adventure_Works_Desktop_App.ProductPage.Frontend;
using System;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.MenuPage
{
    public partial class MenuForm : Form
    {
        private string username;
        
        public MenuForm(string username)
        {
            InitializeComponent();

            this.username = username;
        }

        private void employeeInfoButton_Click(object sender, EventArgs e)
        {
            FormNavigationHelper.ShowFormAndBackButton(this, new EmployeeInfoForm(username));
        }

        private void productsInfoButton_Click(object sender, EventArgs e)
        {
            FormNavigationHelper.ShowFormAndBackButton(this, new ProductInfoForm(username));
        }

        private void addReviewButton_Click(object sender, EventArgs e)
        {
            FormNavigationHelper.ShowFormAndBackButton(this, new ReviewProductForm(username));
        }

        private void salesInfoButton_Click(object sender, EventArgs e)
        {
            FormNavigationHelper.ShowFormAndBackButton(this, new SalesPersonForm(username));
        }

        private void viewStoreDetailsButton_Click(object sender, EventArgs e)
        {
            FormNavigationHelper.ShowFormAndBackButton(this, new StoreDetailListForm(username));
        }

        private void InitalFormLoad(object sender, EventArgs e)
        {
            welcomeLabel.Text = string.Format(Properties.Resources.WelcomeUpdate, username);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
