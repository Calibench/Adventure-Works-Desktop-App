using Adventure_Works_Desktop_App.ProductReviewPage.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Frontend
{
    public partial class ChangeProductForm : Form
    {
        public List<string> productIDs = new List<string>();

        // I am aware that this form is essentially the same as the LanguageSelect one for product details (LanguageProductForm)
        public ChangeProductForm()
        {
            InitializeComponent();
            // populate productComboBox
            ProductNameSelect pns = new ProductNameSelect();
            pns.PopulateComboBox(productComboBox, productIDs);
            productComboBox.SelectedIndex = 0;
        }

        public string GetProductName()
        {
            return productComboBox.Text;
        }

        public string GetProductID()
        { 
            return productIDs.ElementAt(productComboBox.SelectedIndex).ToString();
        }
    }
}
