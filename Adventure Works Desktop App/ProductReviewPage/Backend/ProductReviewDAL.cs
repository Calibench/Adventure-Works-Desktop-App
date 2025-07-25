using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ProductReviewDAL
    {
        #region Review Product Backend

        #region Stored Procedures:
        /// <summary>
        /// Submits a new product review to the database using the specified review data.
        /// </summary>
        /// <remarks>This method executes a stored procedure to insert a new review into the database.
        /// Ensure that the <paramref name="rpd"/> parameter is not null and contains valid data for all required
        /// fields.</remarks>
        /// <param name="rpd">The review data containing details such as product ID, reviewer name, review date, email address, rating,
        /// comments, and modified date.</param>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public void DBSubmitReview(ReviewProductData rpd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspInsertNewReview", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductID", rpd.ProductID);
                        cmd.Parameters.AddWithValue("@ReviewerName", rpd.ReviewerName);
                        cmd.Parameters.AddWithValue("@ReviewDate", rpd.ReviewDate);
                        cmd.Parameters.AddWithValue("@EmailAddress", rpd.EmailAddress);
                        cmd.Parameters.AddWithValue("@Rating", rpd.Rating);
                        cmd.Parameters.AddWithValue("@Comments", rpd.Comments);
                        cmd.Parameters.AddWithValue("@ModifiedDate", rpd.ModifiedDate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in SubmitReview.", ex);
            }
        }

        #endregion

        #region Scalar Functions:
        /// <summary>
        /// Retrieves the case-sensitive display name from the database for the specified review name.
        /// </summary>
        /// <param name="reviewName">The review name for which to retrieve the display name. Cannot be null.</param>
        /// <returns>A string containing the case-sensitive display name if found; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public string DBGetDisplayName(string reviewName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select dbo.ufnGetCaseSensitiveDisplayName(@DisplayName)", con))
                    {
                        cmd.Parameters.AddWithValue("@DisplayName", reviewName);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetCaseSensitiveDisplaName.", ex);
            }
        }

        /// <summary>
        /// Retrieves the email address associated with the specified display name from the database.
        /// </summary>
        /// <param name="displayName">The display name of the user whose email address is to be retrieved. Cannot be null or empty.</param>
        /// <returns>The email address of the user if found; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public string DBGetEmailAddress(string displayName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select dbo.ufnGetUserEmail(@DisplayName)", con))
                    {
                        cmd.Parameters.AddWithValue("@DisplayName", displayName);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetEmailAddress.", ex);
            }
        }

        #endregion

        #endregion

        #region Change Product backend

        public List<string> GetProductNames()
        {
            var productNames = new List<string>();
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetAllProducts", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string product = reader["Name"].ToString();
                                productNames.Add(product);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetProductNames.", ex);
            }
            return productNames;
        }

        public List<string> GetProductIDs()
        {
            var productIDs = new List<string>();
            try
            {
                using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetAllProducts", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productID = reader["ProductID"].ToString();
                                productIDs.Add(productID);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in GetProductIDs.", ex);
            }
            return productIDs;
        }
        #endregion
    }
}
