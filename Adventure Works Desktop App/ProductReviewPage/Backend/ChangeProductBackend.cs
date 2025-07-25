using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ChangeProductBackend
    {
        private readonly ProductReviewDAL _dal = new ProductReviewDAL();

        public void PopulateComboBox(ComboBox comboBox, List<string> productIDs)
        {
            List<string> productNames = _dal.GetProductNames();

            if (productNames == null)
            {
                throw new InvalidOperationException("Unable to continue due to null productNames");
            }

            foreach (string products in productNames)
            { 
                comboBox.Items.Add(products);
            }

            List<string> tempProductID = _dal.GetProductIDs();

            if (tempProductID == null)
            {
                throw new InvalidOperationException("Unable to continue due to null productID");
            }

            foreach (string id in tempProductID)
            {
                productIDs.Add(id);
            }
        }
    }
}
