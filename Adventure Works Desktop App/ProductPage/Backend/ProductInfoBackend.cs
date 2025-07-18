using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.BackEnd
{
    internal class ProductInfoBackend
    {
        private Connection connection = new Connection();
        private List<CustomerReviewData> customerData;
        private List<ProductData> productData;

        public enum Procedure
        {
            Category = 1,
            SubCategory,
            ProductName
        }

        public List<ProductData> ProductData
        {
            get { return productData; }
            set { productData = value; }
        }

        public List<CustomerReviewData> CustomerData
        {
            get { return customerData; }
        }

        public ProductInfoBackend(string cultureName)
        {
            customerData = new List<CustomerReviewData>();
            productData = new List<ProductData>();
            SearchReview();
            string cultureID = GetCultureID(cultureName);
            SearchProduct(cultureID);
        }

        // Put this data into the CustomerReviewData (productID, customerName, date, rating, comment)
        private void SearchReview()
        {
            string query = "select productID, ReviewerName, format(ReviewDate, 'yyyy-MM-dd') as ReviewDate, Rating, Comments " +
                           "from Production.ProductReview;";
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    CustomerReviewData tempCustomerData = new CustomerReviewData($"{reader["productID"]}", $"{reader["ReviewerName"]}",
                                                                            $"{reader["ReviewDate"]}", Convert.ToInt16(reader["Rating"]),
                                                                            $"{reader["Comments"]}");
                    customerData.Add(tempCustomerData);
                }
            }
        }

        private void SearchProduct(string cultureID)
        {
            string query = $"execute dbo.ProductSearchLang @CultureID = {cultureID};";

            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    ProductData tempProductData = new ProductData($"{reader["Product_Catagory"]}", $"{reader["Product_SubCategory"]}",
                        $"{reader["ProductID"]}", $"{reader["ProductNumber"]}", $"{reader["Product_Name"]}",
                        $"{reader["ListPrice"]}", $"{reader["StandardCost"]}", $"{reader["Margin_Profit"]}", $"{reader["Size"]}",
                        $"{reader["Color"]}", $"{reader["Weight"]}", $"{reader["Description"]}", $"{reader["Culture_Name"]}");
                    productData.Add(tempProductData);
                }
            }
        }

        private string GetCultureID(string cultureName)
        {
            string query = $"execute dbo.uspGetCultureID @CultureName = {cultureName}";
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    return $"{reader["CultureID"]}";
                }
                return "error";
            }
        }

        /// <summary>
        /// This gets the appropriate procedure's combobox data
        /// </summary>
        /// <param name="procedure">category, subcategory, productname</param>
        /// <returns></returns>
        public List<string> GetCategories(Procedure procedure, string category)
        {
            List<string> categories = new List<string>();
            string query;
            switch (procedure)
            {
                case Procedure.Category:
                    query = "select Name from Production.ProductCategory";
                    break;
                case Procedure.SubCategory:
                    query = $"execute dbo.uspGetSubcategory @Category = @CategoryParam";
                    break;
                case Procedure.ProductName:
                    query = $"execute dbo.uspGetProductName @SubCategory = @CategoryParam";
                    break;
                default:
                    query = "";
                    MessageBox.Show("ERROR: Procedure does not exist in GetCategories() - ProductInfoBackend.cs");
                    return categories;
            }
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                queryStatus.Parameters.AddWithValue("@CategoryParam", category);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add($"{reader["Name"]}");
                }
            }
            return categories;
        }

        public ProductData GetProductData(string cultureName, string productName)
        {
            foreach(ProductData data in productData)
            {
                if (data.CultureID == cultureName && data.ProductName == productName)
                {
                    return data;
                }
            }
            return null;
        }
    }
}
