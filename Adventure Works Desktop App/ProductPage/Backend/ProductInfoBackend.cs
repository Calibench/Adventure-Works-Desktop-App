using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Adventure_Works_Desktop_App.ProductPage.Backend
{
    internal class ProductInfoBackend
    {
        private List<CustomerReviewData> customerData;
        private List<ProductData> productData;
        private readonly ProductDAL _dal = new ProductDAL();

        public enum Procedure
        {
            Category = 1,
            SubCategory,
            ProductName
        }

        public enum ProductDetailsUpdate
        {
            PrimaryCategoryComboBox = 1,
            SubCategoryComboBox,
            ProductComboBox,
            UpdateProductDetails
        }

        public List<CustomerReviewData> CustomerData
        {
            get { return customerData; }
        }

        public ProductInfoBackend(string cultureName)
        {
            customerData = new List<CustomerReviewData>();
            productData = new List<ProductData>();
            _dal.DBSearchReview(customerData);
            _dal.DBSearchProduct(cultureName, productData);
        }

        /// <summary>
        /// Retrieves a list of category names based on the specified procedure type.
        /// </summary>
        /// <remarks>The method executes different SQL queries based on the specified <paramref
        /// name="procedure"/>.  If <paramref name="procedure"/> is <see cref="Procedure.Category"/>, a text query is
        /// executed to retrieve product categories. For <see cref="Procedure.SubCategory"/> and <see
        /// cref="Procedure.ProductName"/>, stored procedures are executed,  and the <paramref name="category"/>
        /// parameter is used to filter results.</remarks>
        /// <param name="procedure">The type of procedure to execute. Must be one of the defined <see cref="Procedure"/> values.</param>
        /// <param name="category">The category or subcategory name used as a parameter for certain procedures.  This parameter is required
        /// when <paramref name="procedure"/> is <see cref="Procedure.SubCategory"/> or <see
        /// cref="Procedure.ProductName"/>.</param>
        /// <returns>A list of strings containing the names of categories. The list will be empty if no categories are found.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="procedure"/> is not a valid <see cref="Procedure"/> value.</exception>
        public List<string> GetCategories(Procedure procedure, string category)
        {
            List<string> categories = new List<string>();

            categories = _dal.DBGetCategories(GetQueryByProcedure(procedure), procedure, category);

            return categories;
        }

        /// <summary>
        /// Retrieves the product data for a specified culture and product name.
        /// </summary>
        /// <param name="cultureName">The culture identifier used to locate the product data. Cannot be null or empty.</param>
        /// <param name="productName">The name of the product for which data is requested. Cannot be null or empty.</param>
        /// <returns>The <see cref="productData"/> object that matches the specified culture and product name.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no product data is found for the specified <paramref name="cultureName"/> and <paramref
        /// name="productName"/>.</exception>
        public ProductData GetProductData(string cultureName, string productName)
        {
            try
            {
                foreach (ProductData data in productData)
                {
                    if (data.CultureID == cultureName && data.ProductName == productName)
                    {
                        return data;
                    }
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                // refer(https://stackoverflow.com/questions/16434842/invalidoperationexception-vs-argumentexception)
                throw new InvalidOperationException($"No product data found for culture '{cultureName}' and product name '{productName}'.", ex);
            }
        }

        #region Helper Methods

        private string GetQueryByProcedure(Procedure procedure)
        {
            switch (procedure)
            {
                case Procedure.Category:
                    return "dbo.uspGetCategory";
                case Procedure.SubCategory:
                    return "dbo.uspGetSubcategory";
                case Procedure.ProductName:
                    return "dbo.uspGetProductName";
                default:
                    throw new ArgumentException("Invalid procedure type.", nameof(procedure));
            }
        }

        public static string TrimSpacesBetweenString(string s)
        {
            return Regex.Replace(s, @"\s{2,}", " ");
        }

        public static string GetFirst(ComboBox combo)
        {
            return combo.Items[0].ToString();
        }

        #endregion
    }
}
