using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.Backend
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

        public List<CustomerReviewData> CustomerData
        {
            get { return customerData; }
        }

        public ProductInfoBackend(string cultureName)
        {
            customerData = new List<CustomerReviewData>();
            productData = new List<ProductData>();
            SearchReview();
            SearchProduct(cultureName);
        }

        // Put this data into the CustomerReviewData (productID, customerName, date, rating, comment)
        /// <summary>
        /// Retrieves customer reviews from the database and populates the customer data collection.
        /// </summary>
        /// <remarks>This method executes a stored procedure to fetch customer reviews, which are then
        /// added to the <paramref name="customerData"/> collection. Each review includes details such as product ID, reviewer name,
        /// review date, rating, and comments.</remarks>
        private void SearchReview()
        {
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("dbo.uspGetCustomerReviews", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerReviewData tempCustomerData = new CustomerReviewData(
                                reader["productID"].ToString(), reader["ReviewerName"].ToString(),
                                reader["ReviewDate"].ToString(), Convert.ToInt16(reader["Rating"]),
                                reader["Comments"].ToString());
                            customerData.Add(tempCustomerData);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches for product data based on the specified culture name.
        /// </summary>
        /// <remarks>This method executes a stored procedure to retrieve product information localized to
        /// the specified culture. The results are added to the <paramref name="productData"/> collection.</remarks>
        /// <param name="cultureName">The name of the culture used to filter the product data. This parameter cannot be null or empty.</param>
        private void SearchProduct(string cultureName)
        {
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.uspProductSearchLang", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cultureName", cultureName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductData tempProductData = new ProductData(
                                reader["Product_Catagory"].ToString(), reader["Product_SubCategory"].ToString(),
                                reader["ProductID"].ToString(), reader["ProductNumber"].ToString(), reader["Product_Name"].ToString(),
                                reader["ListPrice"].ToString(), reader["StandardCost"].ToString(), reader["Margin_Profit"].ToString(), 
                                reader["Size"].ToString(), reader["Color"].ToString(), reader["Weight"].ToString(), 
                                reader["Description"].ToString(), reader["Culture_Name"].ToString());
                            productData.Add(tempProductData);
                        }
                    }
                }
            }
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
            string query;
            CommandType commandType = CommandType.StoredProcedure;
            switch (procedure)
            {
                case Procedure.Category:
                    query = "select Name from Production.ProductCategory";
                    commandType = CommandType.Text;
                    break;
                case Procedure.SubCategory:
                    query = "dbo.uspGetSubcategory";
                    break;
                case Procedure.ProductName:
                    query = "dbo.uspGetProductName";
                    break;
                default:
                    throw new ArgumentException("Invalid procedure type.", nameof(procedure));
            }
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = commandType;

                    if (procedure == Procedure.SubCategory)
                    {
                        cmd.Parameters.AddWithValue("@Category", category);
                    }
                    else if (procedure == Procedure.ProductName)
                    {
                        cmd.Parameters.AddWithValue("@SubCategory", category);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(reader["Name"].ToString());
                        }
                    }
                    return categories;
                }   
            }
            throw new Exception("--Unable to connect to DB--");
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
            foreach (ProductData data in productData)
            {
                if (data.CultureID == cultureName && data.ProductName == productName)
                {
                    return data;
                }
            }
            // refer(https://stackoverflow.com/questions/16434842/invalidoperationexception-vs-argumentexception)
            throw new InvalidOperationException($"No product data found for culture '{cultureName}' and product name '{productName}'.");
        }
    }
}
