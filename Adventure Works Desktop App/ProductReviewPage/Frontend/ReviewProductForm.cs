using Adventure_Works_Desktop_App.ProductReviewPage.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Frontend
{
    public partial class ReviewProductForm : Form
    {
        public bool backButtonPressed = false;
        private string username;
        public ReviewProductForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        // Default Event(s)
        private void backButton_Click(object sender, EventArgs e)
        {
            backButtonPressed = true;
            this.Close();
        }
        // End of Default Event(s)

        // Start of Events
        private void ProductNameMouseEnters(object sender, EventArgs e)
        {
            //productNameValueLabel
            //underline the text when entered with mouse
            productNameValueLabel.Font = new Font(productNameValueLabel.Font, FontStyle.Underline);
        }
        private void ProductNameMouseLeaves(object sender, EventArgs e)
        {
            productNameValueLabel.Font = new Font(productNameValueLabel.Font, FontStyle.Regular);
        }
        private void ProductNameValueLabelClicked(object sender, EventArgs e)
        {
            ChangeProductForm frm = new ChangeProductForm();
            frm.ShowDialog();

            if (frm.backButton)
            {
                if (!productIDValueLabel.Visible)
                {
                    productIDLabel.Visible = true;
                    productIDValueLabel.Visible = true;
                    ratingPanel.Visible = true;
                    reviewPanel.Visible = true;
                    submitButton.Visible = true;
                }
                productNameValueLabel.Text = frm.GetProductName();
                productIDValueLabel.Text = frm.GetProductID();
                threeStarRadioButton.Select();
                customerReviewRichTextBox.Clear();
            }
        }

        private void ProductIDMouseEnter(object sender, EventArgs e)
        {
            //productIDValueLabel
            productIDValueLabel.Font = new Font(productIDValueLabel.Font, FontStyle.Underline);
        }
        private void ProductIDMouseLeave(object sender, EventArgs e)
        {
            productIDValueLabel.Font = new Font(productIDValueLabel.Font, FontStyle.Regular);
        }
        private void ProductIDValueLabelClicked(object sender, EventArgs e)
        {
            ProductNameValueLabelClicked(sender, e);
        }

        private void SubitButtonClicked(object sender, EventArgs e)
        {
            if (customerReviewRichTextBox.Text.Length > 25)
            {
                //string productID, string rating, string reviewName, string comments
                var ratingRadio = ratingPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                int rating = 0;
                switch (ratingRadio.Text)
                {
                    case "One Star":
                        rating = 1;
                        break;
                    case "Two Star":
                        rating = 2;
                        break;
                    case "Three Star":
                        rating = 3;
                        break;
                    case "Four Star":
                        rating = 4;
                        break;
                    case "Five Star":
                        rating = 5;
                        break;
                    default:
                        rating = 0;
                        break;
                }

                new ReviewProductBackend(productIDValueLabel.Text, rating, usernameLabel.Text, customerReviewRichTextBox.Text);
                backButtonPressed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a review of at least 25 characters");
            }
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            usernameLabel.Text = $"Logged in: {username}";

            // ensure everything is invisible upon entering till the product has been selected
            productIDLabel.Visible = false;
            productIDValueLabel.Visible = false;
            ratingPanel.Visible = false;
            reviewPanel.Visible = false;
            submitButton.Visible = false;
        }
        //End of Events
    }
}
