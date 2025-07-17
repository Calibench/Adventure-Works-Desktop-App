using Adventure_Works_Desktop_App.ProductReviewPage.Frontend;
using Adventure_Works_Desktop_App.SalesPersonPage.Frontend;
using Adventure_Works_Desktop_App.StoreDetailsPage.Frontend;
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
            ShowForm(new EmployeeInfoForm(username));
        }

        private void productsInfoButton_Click(object sender, EventArgs e)
        {
            ShowForm(new ProductInfoForm(username));
        }

        private void addReviewButton_Click(object sender, EventArgs e)
        {
            ShowForm(new ReviewProductForm(username));
        }

        private void salesInfoButton_Click(object sender, EventArgs e)
        {
            ShowForm(new SalesPersonForm(username));
        }

        private void viewStoreDetailsButton_Click(object sender, EventArgs e)
        {
            ShowForm(new StoreDetailListForm(username));
        }

        private void InitalFormLoad(object sender, EventArgs e)
        {
            welcomeLabel.Text = $"Welcome, {username}";
        }

        private void ShowForm(Form form)
        {
            form.Location = this.Location;
            this.Hide();
            form.ShowDialog();
            this.Location = form.Location;

            if (form is EmployeeInfoForm eif && eif.backButtonPressed)
            {
                this.Show();
            }
            else if (form is ProductInfoForm rif && rif.backButtonPressed)
            {
                this.Show();
            }
            else if (form is ReviewProductForm rpf && rpf.backButtonPressed)
            {
                this.Show();
            }
            else if (form is SalesPersonForm spf && spf.backButtonPressed)
            {
                this.Show();
            }
            else if (form is StoreDetailListForm sdf && sdf.backButtonPressed)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }
    }
}
