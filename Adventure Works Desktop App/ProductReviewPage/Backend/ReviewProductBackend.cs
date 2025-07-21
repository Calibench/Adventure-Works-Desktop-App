using Adventure_Works_Desktop_App.Globals.DataClasses;
using System.Data.SqlClient;
using System;
using System.Data;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ReviewProductBackend
    {
        Connection connection = new Connection();
        public ReviewProductBackend(string productID, int rating, string reviewName, string comments)
        {
            string displayName = GetCaseSensitiveDisplayName(TrimUsername(reviewName));
            ReviewProductData rpd = new ReviewProductData(productID, rating, displayName, comments, GetEmailAddress(displayName));
            SubmitReview(rpd);
        }

        /// <summary>
        /// Retrieves the email address associated with the specified display name.
        /// </summary>
        /// <param name="displayName">The display name of the user whose email address is to be retrieved. Cannot be null or empty.</param>
        /// <returns>The email address of the user if found.</returns>
        /// <exception cref="ArgumentException">Thrown if the email address cannot be retrieved for the specified display name.</exception>
        private string GetEmailAddress(string displayName)
        {
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
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
            throw new ArgumentException(Properties.ProductReviewResources.UnableToGetEmail, displayName);
        }

        /// <summary>
        /// Retrieves the case-sensitive display name corresponding to the specified review name.
        /// </summary>
        /// <param name="reviewName">The review name for which to retrieve the case-sensitive username. Cannot be null or empty.</param>
        /// <returns>The case-sensitive display name if found; otherwise, throws an exception.</returns>
        /// <exception cref="ArgumentException">Thrown if the case-sensitive username cannot be retrieved for the specified <paramref name="reviewName"/>.</exception>
        private string GetCaseSensitiveDisplayName(string reviewName)
        {
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
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
            throw new ArgumentException(Properties.ProductReviewResources.UnableToGetDisplayName, reviewName);
        }

        /// <summary>
        /// Trims the specified username by removing any prefix up to and including the first colon.
        /// </summary>
        /// <param name="reviewName">The username string to be trimmed. Must contain a colon followed by at least one character.</param>
        /// <returns>The trimmed username, starting from the character immediately after the first colon.</returns>
        private string TrimUsername(string reviewName)
        {
            return reviewName.Substring(reviewName.IndexOf(":") + 2);
        }

        /// <summary>
        /// Submits a new product review to the database.
        /// </summary>
        /// <remarks>This method uses a stored procedure to insert a new review into the database. Ensure
        /// that all fields in <paramref name="rpd"/> are populated correctly before calling this method.</remarks>
        /// <param name="rpd">The data for the product review, including product ID, reviewer name, review date, email address, rating,
        /// comments, and modified date.</param>
        private void SubmitReview(ReviewProductData rpd)
        {
            using (SqlConnection con = new SqlConnection(connection.ConnectionString))
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
    }
}
