using Adventure_Works_Desktop_App.ProductReviewPage.Backend;
using System;
using System.CodeDom;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Frontend
{
    public partial class ReviewProductForm : Form
    {
        private string username;
        public ReviewProductForm(string username)
        {
            InitializeComponent();
            this.username = username;
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

        // Start of Events
        private void ProductNameValueLabelClicked(object sender, EventArgs e)
        {
            ChangeProductForm frm = new ChangeProductForm();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
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

        private new void MouseEnter(object sender, EventArgs e)
        {
            var label = sender as Label;
            if (label == null)
            {
                return;
            }
            label.Font = new Font(label.Font, FontStyle.Underline);
        }

        private new void MouseLeave(object sender, EventArgs e)
        {
            var label = sender as Label;
            if (label == null)
            {
                return;
            }
            label.Font = new Font(label.Font, FontStyle.Regular);
        }

        private void SubmitButtonClicked(object sender, EventArgs e)
        {
            const int MIN_REVIEW_LENGTH = 25;

            // Written review
            if (customerReviewRichTextBox.Text.Length <= MIN_REVIEW_LENGTH)
            {
                MessageBox.Show(Properties.ProductReviewResources.InvalidReview);
                return;
            }

            if (string.IsNullOrWhiteSpace(productNameValueLabel.Text))
            {
                MessageBox.Show(Properties.ProductReviewResources.InvalidProductSelection);
                return;
            }

            // Rating
            var ratingRadio = ratingPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (ratingRadio == null)
            {
                MessageBox.Show(Properties.ProductReviewResources.InvalidRating);
                return;
            }

            int rating = 0;
            switch (ratingRadio.Text)
            {
                case "One Star": rating = 1; break;
                case "Two Star": rating = 2; break;
                case "Three Star": rating = 3; break;
                case "Four Star": rating = 4; break;
                case "Five Star": rating = 5; break;
                default: rating = 0; break;
            }

            if (rating == 0)
            {
                MessageBox.Show(Properties.ProductReviewResources.InvalidRating);
                return;
            }

            // Submission
            try
            {
                new ReviewProductBackend(productIDValueLabel.Text, rating, usernameLabel.Text, customerReviewRichTextBox.Text);
                MessageBox.Show(Properties.ProductReviewResources.SuccessfulSubmit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.ProductReviewResources.SubmitError + Environment.NewLine + ex.Message);
            }
        }
        //End of Events
    }
}
