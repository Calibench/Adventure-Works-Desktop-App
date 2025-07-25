using Adventure_Works_Desktop_App.Globals.DataClasses;
using Adventure_Works_Desktop_App.ProductPage.Backend;
using static Adventure_Works_Desktop_App.ProductPage.Backend.ProductInfoBackend;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.Frontend
{
    public partial class ProductInfoForm : Form
    {
        private string displayName;

        public ProductInfoForm(string displayName)
        {
            InitializeComponent();
            this.displayName = displayName;
        }

        private void InitialFormLoad(object sender, EventArgs e)
        {
            loggedBackUserControl.ChangeDisplayName(displayName);
            subCategoryComboBox.Enabled = false;
            productComboBox.Enabled = false;
            UpdateProducts(ProductDetailsUpdate.PrimaryCategoryComboBox);
            cultureNameLabel.Visible = false;
        }

        #region Events Section
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            subCategoryComboBox.Enabled = true;
            productComboBox.Enabled = true;
            subCategoryComboBox.Items.Clear();
            productComboBox.Items.Clear();
            UpdateProducts(ProductDetailsUpdate.SubCategoryComboBox);
            subCategoryComboBox.Text = GetFirst(subCategoryComboBox); // gets first in the combobox
            productComboBox.Text = GetFirst(productComboBox); // gets first in the combobox
            cultureNameLabel.Visible = true;
        }

        private void subCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            productComboBox.Items.Clear();
            UpdateProducts(ProductDetailsUpdate.ProductComboBox);
            productComboBox.Text = GetFirst(productComboBox); // gets first in the combobox
        }

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProducts(ProductDetailsUpdate.UpdateProductDetails);
        }

        #region Lang Section
        private void LanguageLabelClicked(object sender, EventArgs e)
        {
            // open a box that allows user to select the language of choice (this will then filter the sql query to that language through cultureID)
            var langWin = new LanguageProductForm(cultureNameLabel.Text);
            langWin.ShowDialog();
            // checks if the back button was pressed instead of the window was just closed
            if (langWin.DialogResult == DialogResult.OK)
            {
                cultureNameLabel.Text = langWin.GetSelectedLanguage();
                UpdateProducts(ProductDetailsUpdate.UpdateProductDetails);
            }
        }

        private void LanguageLabelEnter(object sender, EventArgs e)
        {
            // allow tooltip to appear
            langToolTip.Active = true;
            // underline the text for the language when hovered
            cultureNameLabel.Font = new Font(cultureNameLabel.Font.Name, cultureNameLabel.Font.SizeInPoints, FontStyle.Underline);
            // tooltip to display
            langToolTip.SetToolTip(cultureNameLabel, "Click to change description language for products");
        }

        private void LanguageLabelLeave(object sender, EventArgs e)
        {
            // changes font back to normal
            cultureNameLabel.Font = new Font(cultureNameLabel.Font.Name, cultureNameLabel.Font.SizeInPoints, FontStyle.Regular);
            // disabling the tooltip prevents ghosting
            langToolTip.Active = false;
        }
        #endregion
        #endregion

        #region Product Details
        /// <summary>
        /// Updates the product details UI components based on the specified update procedure.
        /// </summary>
        /// <remarks>This method handles different update scenarios for product details, such as updating
        /// category and product comboboxes or refreshing product details based on user selections. It ensures that the
        /// UI reflects the current state of product information as provided by the backend.</remarks>
        /// <param name="backend">The backend service providing product information and categories.</param>
        /// <param name="procedure">The specific update procedure to execute, determining which UI component to update.</param>
        private void ProductDetailsProcedureUpdate(ProductInfoBackend backend, ProductDetailsUpdate procedure)
        {
            switch (procedure)
            {
                // update primary combobox - this is upon initial enter of the form
                case ProductDetailsUpdate.PrimaryCategoryComboBox:
                    UpdateComboBox(backend.GetCategories(Procedure.Category, "Nothing"), categoryComboBox);
                    break;
                // update secondary combobox - when category combobox gets a select update choices for secondary combobox
                case ProductDetailsUpdate.SubCategoryComboBox:
                    UpdateComboBox(backend.GetCategories(Procedure.SubCategory, categoryComboBox.Text), subCategoryComboBox);
                    break;
                // update product combobox - when secondary combobox gets a select update choices for product combobox
                case ProductDetailsUpdate.ProductComboBox:
                    UpdateComboBox(backend.GetCategories(Procedure.ProductName, subCategoryComboBox.Text), productComboBox);
                    break;
                // update product details to match the select product name from the product combobox
                case ProductDetailsUpdate.UpdateProductDetails:
                    UpdateProductDetails(backend.GetProductData(cultureNameLabel.Text, productComboBox.Text));
                    break;
            }
        }

        /// <summary>
        /// Updates the products info that is being displayed.
        /// </summary>
        /// <param name="prodData">Product data being parsed to update text</param>
        private void UpdateProductDetails(ProductData prodData)
        {
            if (prodData == null)
            {
                throw new Exception("Unable to update product details due to no product information");
            }

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

        /// <summary>
        /// Updates the specified <see cref="ComboBox"/> by adding items from the provided list that are not already
        /// present.
        /// </summary>
        /// <remarks>This method iterates through the <paramref name="dataList"/> and adds each item to
        /// the <paramref name="combobox"/> only if it is not already present in the <see cref="ComboBox.Items"/>
        /// collection.</remarks>
        /// <param name="dataList">A list of strings representing the items to be added to the <paramref name="combobox"/>.</param>
        /// <param name="combobox">The <see cref="ComboBox"/> to be updated with new items from <paramref name="dataList"/>.</param>
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
        #endregion

        #region Customer Review Section
        /// <summary>
        /// Updates the customer review panel with the provided customer review data.
        /// </summary>
        /// <remarks>This method clears the existing reviews from the customer review panel and populates
        /// it with reviews that match the current product ID</remarks>
        /// <param name="customerData">A list of <see cref="CustomerReviewData"/> objects representing customer reviews to be displayed. Only
        /// reviews matching the current product ID are considered.</param>
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

        /// <summary>
        /// Adds a review to the Customer Review Group Box Section.
        /// </summary>
        /// <param name="numReviews">The number of reviews to go through</param>
        /// <param name="customerReview">Customer review data</param>
        private void AddReview(int numReviews, CustomerReviewData[] customerReview)
        {
            // If there are no reviews then dont update
            if (numReviews == 0)
            {
                return;
            }

            // Default box to get loc
            GroupBox initialGroupBox = new GroupBox();

            // Create the first review
            initialGroupBox = CreateCustomerGroupBox(initialGroupBox, customerReview[0].CustomerName, 
                                                    new Point(initialGroupBox.Location.X, initialGroupBox.Location.Y), 5, 0);
            customerReviewPanel.Controls.Add(initialGroupBox);
            FillDataInCustomerReview(initialGroupBox, customerReview[0]);
            
            // Stores the location of the current indexed groupbox (this is so they can be spaced)
            Point currLocation = new Point(initialGroupBox.Location.X, initialGroupBox.Location.Y);

            // Add additional reviews if necessary
            for (int i = 1; i < numReviews; i++)
            {
                GroupBox groupboxAdditionalCustomer = new GroupBox();
                groupboxAdditionalCustomer = CreateCustomerGroupBox(groupboxAdditionalCustomer, customerReview[i].CustomerName, 
                                                                    currLocation, 0, 100);
                customerReviewPanel.Controls.Add(groupboxAdditionalCustomer);
                FillDataInCustomerReview(groupboxAdditionalCustomer, customerReview[i]);
                currLocation = new Point(groupboxAdditionalCustomer.Location.X, groupboxAdditionalCustomer.Location.Y);
            }
        }

        /// <summary>
        /// Individual customer review boxes
        /// </summary>
        /// <param name="groupBox">Groupbox to hold all customer reviews</param>
        /// <param name="customerName">Customer name to be the title of the groupbox</param>
        /// <param name="currLocation">Used to track current location</param>
        /// <param name="x">x-axis spacing</param>
        /// <param name="y">y-axis spacing</param>
        /// <returns>Newly created customer review</returns>
        private GroupBox CreateCustomerGroupBox(GroupBox groupBox, string customerName, Point currLocation, int x, int y)
        {
            // following the same font as the form is currently using
            Font smallFont = new Font(customerReviewGroupBox.Font.Name, 8);
            Size sizeOfGroupBox = new Size(410, 100);

            groupBox.Text = customerName;
            groupBox.Font = smallFont;
            groupBox.Size = sizeOfGroupBox;
            groupBox.BringToFront();
            groupBox.Location = new Point(currLocation.X + x, currLocation.Y + y);

            return groupBox;
        }

        /// <summary>
        /// Creates a customer review and fills in the necessary data.
        /// </summary>
        /// <param name="groupBox">Customer Review GroupBox, this is a unique box per customer review</param>
        /// <param name="customerReview">Data needing to be populated</param>
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
        #endregion

        #region Helper Methods
        /// <summary>
        /// Wrapper to update front end details
        /// </summary>
        /// <param name="numProductDetailsUpdate">1=update primcatcombo, 2=update subcatcombo, 3=update productcombo, 4=update product details</param>
        private void UpdateProducts(ProductDetailsUpdate numProductDetailsUpdate)
        {
            // to do the reviews, use this container (customerReviewPanel) encapsulate it with a groupbox
            ProductInfoBackend backend = new ProductInfoBackend(cultureNameLabel.Text);

            ProductDetailsProcedureUpdate(backend, numProductDetailsUpdate);
            CustomerReviewUpdate(backend.CustomerData);
        }
        #endregion
    }
}
