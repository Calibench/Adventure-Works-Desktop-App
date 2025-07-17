using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App
{
    public partial class ProductInfoForm : Form
    {
        public bool backButtonPressed = false;
        private string username;
        public ProductInfoForm(String username)
        {
            InitializeComponent();
            this.username = username;
        }

        // Start Event Handler Section
        private void backButton_Click(object sender, EventArgs e)
        {
            backButtonPressed = true;
            this.Close();
        }
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            subCategoryComboBox.Enabled = true;
            productComboBox.Enabled = true;
            subCategoryComboBox.Items.Clear();
            productComboBox.Items.Clear();
            UpdateProducts(2);
            subCategoryComboBox.Text = GetFirst(subCategoryComboBox); // replace this with the first in the list use a func
            productComboBox.Text = GetFirst(productComboBox); // replace this with the first in the list use a func
        }

        private void subCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            productComboBox.Items.Clear();
            UpdateProducts(3);
            productComboBox.Text = GetFirst(productComboBox); // replace this with the first in the list use a func
        }

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProducts(4);
        }
        // End Event Handler Section

        // Start Lang Section
        private void LanguageLabelClicked(object sender, EventArgs e)
        {
            // open a box that allows user to select the language of choice (this will then filter the sql query to that language through cultureID)
            var langWin = new LanguageProductForm(cultureIDLabel.Text);
            langWin.ShowDialog();
            // checks if the back button was pressed instead of the window was just closed
            if (langWin.backButton && categoryComboBox.Text != "")
            {
                cultureIDLabel.Text = langWin.GetSelectedLanguage();
                UpdateProducts(4);
            }
            else 
            {
                cultureIDLabel.Text = langWin.GetSelectedLanguage();
            }
        }

        private void LanguageLabelEnter(object sender, EventArgs e)
        {
            // allow tooltip to appear
            langToolTip.Active = true;
            // underline the text for the language when hovered
            cultureIDLabel.Font = new Font(cultureIDLabel.Font.Name, cultureIDLabel.Font.SizeInPoints, FontStyle.Underline);
            // tooltip to display
            langToolTip.SetToolTip(cultureIDLabel, "Click to change description language for products");
        }

        private void LanguageLabelLeave(object sender, EventArgs e)
        {
            // changes font back to normal
            cultureIDLabel.Font = new Font(cultureIDLabel.Font.Name, cultureIDLabel.Font.SizeInPoints, FontStyle.Regular);
            // disabling the tooltip prevents ghosting
            langToolTip.Active = false;
        }
        // End Lang Section

        /// <summary>
        /// Wrapper to update front end details
        /// </summary>
        /// <param name="numProductDetailsUpdate">1=update primcatcombo, 2=update subcatcombo, 3=update productcombo, 4=update product details</param>
        private void UpdateProducts(int numProductDetailsUpdate)
        {
            // to do the reviews, use this container (customerReviewPanel) encapsulate it with a groupbox
            ProductInfoBackend backend = new ProductInfoBackend(cultureIDLabel.Text);
            
            ProductDetailsProcedureUpdate(backend, numProductDetailsUpdate);
            CustomerReviewUpdate(backend.CustomerData);
        }

        // Start of Product Details
        private void ProductDetailsProcedureUpdate(ProductInfoBackend backend, int procedure)
        {
            switch (procedure)
            {
                // update primary combobox - this is upon initial enter of the form
                case 1:
                    UpdateComboBox(backend.GetCategories(1, "Nothing"), categoryComboBox);
                    break;
                // update secondary combobox - when category combobox gets a select update choices for secondary combobox
                case 2:
                    UpdateComboBox(backend.GetCategories(2, categoryComboBox.Text), subCategoryComboBox);
                    break;
                // update product combobox - when secondary combobox gets a select update choices for product combobox
                case 3:
                    UpdateComboBox(backend.GetCategories(3, subCategoryComboBox.Text), productComboBox);
                    break;
                // update product details to match the select product name from the product combobox
                case 4:
                    UpdateProductDetails(backend.GetProductData(cultureIDLabel.Text, productComboBox.Text));
                    break;
            }
        }

        private void UpdateProductDetails(ProductData prodData)
        {
            productNNSGroupBox.Visible = true;
            productID.Text = prodData.ProductID;
            productNNSGroupBox.Text = $"({prodData.ProductNumber}) - {prodData.ProductName}";
            sizeValueLabel.Text = prodData.Size;
            sizeValueLabel.TextAlign = ContentAlignment.MiddleRight;
            colorValueLabel.Text = prodData.Color;
            colorValueLabel.TextAlign = ContentAlignment.MiddleRight;
            weightValueLabel.Text = prodData.Weight;
            weightValueLabel.TextAlign = ContentAlignment.MiddleRight;
            productDescriptionRichTextBox.Text = prodData.Description;
            costValueLabel.Text = prodData.StandardCost;
            listingPriceValueLabel.Text = prodData.ListPrice;
            profitValueLabel.Text = prodData.MarginProfit;
        }

        private void UpdateComboBox(List<string> dataList, ComboBox combobox)
        {
            foreach(string data in dataList)
            {
                if(!combobox.Items.Contains(data))
                {
                    combobox.Items.Add(data);
                }    
            }
        }

        // End of Product Details

        // Start of Customer Reviews
        private void CustomerReviewUpdate(List<CustomerReviewData> customerData)
        {
            // clear panel before updating
            customerReviewPanel.Controls.Clear();

            List<CustomerReviewData> customerReviewDatas = new List<CustomerReviewData>();
            foreach (CustomerReviewData customerReview in customerData)
            {
                if (customerReview != null && customerReview.ProductID == productID.Text)
                {
                    customerReviewDatas.Add(customerReview);
                }
            }
            AddReview(customerReviewDatas.Count, customerReviewDatas.ToArray());
        }

        private void AddReview(int numReviews, CustomerReviewData[] customerReview)
        {
            // default box to get loc
            GroupBox initialGroupBox = new GroupBox();
            
            // if there are no reviews then dont update
            if (numReviews == 0)
            {
                return;
            }
            // else create the first review
            else
            {
                initialGroupBox = CreateCustomerGroupBox(initialGroupBox, customerReview[0].CustomerName, 
                                  new Point(initialGroupBox.Location.X, initialGroupBox.Location.Y), 5, 0);
                customerReviewPanel.Controls.Add(initialGroupBox);
                FillDataInCustomerReview(initialGroupBox, customerReview[0]);
            }
            
            // stores the location of the current indexed groupbox (this is so they can be spaced)
            Point currLocation = new Point(initialGroupBox.Location.X, initialGroupBox.Location.Y);

            // add additional reviews if necessary
            for (int i = 1; i < numReviews; i++)
            {
                GroupBox groupboxAdditionalCustomer = new GroupBox();
                groupboxAdditionalCustomer = CreateCustomerGroupBox(groupboxAdditionalCustomer, customerReview[i].CustomerName, currLocation, 0, 100);
                customerReviewPanel.Controls.Add(groupboxAdditionalCustomer);
                FillDataInCustomerReview(groupboxAdditionalCustomer, customerReview[i]);
                currLocation = new Point(groupboxAdditionalCustomer.Location.X, groupboxAdditionalCustomer.Location.Y);
            }
        }

        private GroupBox CreateCustomerGroupBox(GroupBox groupBox, string text, Point currLocation, int x, int y)
        {
            // following the same font as the form is currently using
            Font smallFont = new Font(customerReviewGroupBox.Font.Name, 8);
            Size sizeOfGroupBox = new Size(410, 100);

            groupBox.Text = text;
            groupBox.Font = smallFont;
            groupBox.Size = sizeOfGroupBox;
            groupBox.BringToFront();
            groupBox.Location = new Point(currLocation.X + x, currLocation.Y + y);

            return groupBox;
        }

        private void FillDataInCustomerReview(GroupBox groupBox, CustomerReviewData customerReview)
        {
            // now add data inside the groupbox
            
            // Date Label (top left)
            Label reviewDateLabel = new Label();
            reviewDateLabel.Text = customerReview.Date;
            reviewDateLabel.Location = new Point(8, 15);
            reviewDateLabel.BringToFront();
            groupBox.Controls.Add(reviewDateLabel);

            // Rating Label (top right)
            Label ratingLabel = new Label();
            ratingLabel.Text = $"{customerReview.Rating} / 5";
            ratingLabel.Location = new Point(300, 15);
            ratingLabel.BringToFront();
            groupBox.Controls.Add(ratingLabel);

            // Review Comment (Bottom)
            RichTextBox commentRichTextBox = new RichTextBox();
            commentRichTextBox.ReadOnly = true;
            commentRichTextBox.Text = TrimSpacesBetweenString(customerReview.Comment);
            commentRichTextBox.Location = new Point(3, 38);
            commentRichTextBox.Size = new Size(400, 56);
            commentRichTextBox.BringToFront();
            groupBox.Controls.Add(commentRichTextBox);
        }
        // End of Customer Reviews

        // Helper Functions:
        /// <summary>
        /// Helper function to rid of extra spaces between words (used in comments, as in db double spaces are apparent).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimSpacesBetweenString(string s)
        {
            //var mystring = s.Split(new string[] { " " }, StringSplitOptions.None);
            //string result = string.Empty;
            //foreach (var mstr in mystring)
            //{
            //    var ss = mstr.Trim();
            //    if (!string.IsNullOrEmpty(ss))
            //    {
            //        result = result + ss + " ";
            //    }
            //}
            //return result.Trim();

            return Regex.Replace(s, @"\s{2,}", " ");
        }

        /// <summary>
        /// Get first key in newly populated combobox
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetFirst(ComboBox combo)
        {
            return combo.Items[0].ToString();
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            usernameLabel.Text = $"Logged in: {username}";
            subCategoryComboBox.Enabled = false;
            productComboBox.Enabled = false;
            UpdateProducts(1);
        }
    }
}
