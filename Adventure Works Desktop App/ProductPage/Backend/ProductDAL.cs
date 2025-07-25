using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using static Adventure_Works_Desktop_App.ProductPage.Backend.ProductInfoBackend;

namespace Adventure_Works_Desktop_App.ProductPage.Backend
{
    internal class ProductDAL
    {
        #region Product Info Backend

        #region Stored Procedures:
        /// <summary>
        /// Retrieves customer reviews from the database and populates the provided list with the results.
        /// </summary>
        /// <remarks>This method executes a stored procedure to fetch customer reviews from the database. 
        /// Ensure that the connection string "AdventureWorksDb" is correctly configured in the application
        /// settings.</remarks>
        /// <param name="customerData">A list to be populated with <see cref="CustomerReviewData"/> objects representing customer reviews. The list
        /// will be cleared and filled with the results from the database.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is an error accessing the database.</exception>
        public void DBSearchReview(List<CustomerReviewData> customerData)
        {
            if (customerData == null)
            {
                throw new ArgumentNullException(nameof(customerData));
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetCustomerReviews", con))
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
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBSearchReview.", ex);
            }
        }

        /// <summary>
        /// Executes a stored procedure to search for products based on the specified culture name and populates the
        /// provided list with the results.
        /// </summary>
        /// <remarks>This method connects to the AdventureWorks database and executes the stored procedure
        /// 'dbo.uspProductSearchLang'. The results are read and added to the <paramref name="productData"/> list.
        /// Ensure that the connection string 'AdventureWorksDb' is correctly configured in the application
        /// settings.</remarks>
        /// <param name="cultureName">The culture name used to filter the product search results. Cannot be null or empty.</param>
        /// <param name="productData">A list to be populated with the product data retrieved from the database. Must not be null.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public void DBSearchProduct(string cultureName, List<ProductData> productData)
        {
            if (string.IsNullOrWhiteSpace(cultureName))
            {
                throw new ArgumentException("cultureName cannot be null or empty.", nameof(cultureName));
            }

            if (productData == null)
            {
                throw new ArgumentNullException(nameof(productData));
            }

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
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
                                    reader["Product_Category"].ToString(), reader["Product_SubCategory"].ToString(),
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
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBSearchProduct.", ex);
            }
        }

        /// <summary>
        /// Retrieves a list of category names from the database based on the specified query and procedure type.
        /// </summary>
        /// <remarks>The method opens a connection to the database, executes the specified stored
        /// procedure, and reads the category names from the result set. The <paramref name="procedure"/> parameter
        /// determines whether the <paramref name="value"/> is treated as a category or subcategory.</remarks>
        /// <param name="query">The SQL query to execute, which should correspond to a stored procedure.</param>
        /// <param name="procedure">The type of procedure to execute, determining which parameter to add to the command.</param>
        /// <param name="value">The value to be used as a parameter in the stored procedure, representing either a category or subcategory.</param>
        /// <param name="categories">A list to which the retrieved category names will be added.</param>
        /// <returns>A list of category names retrieved from the database. The list will contain the names of categories or
        /// subcategories based on the procedure type.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure in accessing the database.</exception>
        public List<string> DBGetCategories(string query, Procedure procedure, string value)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("cultureName cannot be null or empty.", nameof(query));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value cannot be null or empty.", nameof(value));
            }

            List<string> categories = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (procedure == Procedure.SubCategory)
                        {
                            cmd.Parameters.AddWithValue("@Category", value);
                        }
                        else if (procedure == Procedure.ProductName)
                        {
                            cmd.Parameters.AddWithValue("@SubCategory", value);
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
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBGetCategories.", ex);
            }
        }
        #endregion

        #endregion

        #region Product Language Backend

        #region Stored Procedures:
        /// <summary>
        /// Retrieves a list of language names from the database, excluding Spanish.
        /// </summary>
        /// <remarks>This method executes a stored procedure to obtain language names that are not
        /// Spanish. It establishes a connection to the database using the connection string specified in the
        /// application's configuration file.</remarks>
        /// <returns>A list of strings containing the names of languages retrieved from the database. The list will be empty if
        /// no languages are found.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public List<string> DBRetrieveLanguages()
        {
            var languages = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetAllNotSpanishLanguages", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                languages.Add(reader["Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in DBRetrieveLanguages.", ex);
            }
            return languages;
        }
        #endregion

        #endregion
    }
}
