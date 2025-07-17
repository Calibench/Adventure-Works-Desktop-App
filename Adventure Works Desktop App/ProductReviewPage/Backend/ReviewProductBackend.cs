using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ReviewProductBackend
    {
        Connection connection = new Connection();
        public ReviewProductBackend(string productID, int rating, string reviewName, string comments)
        {
            string displayName = GetCaseSensitiveUsername(TrimUsername(reviewName));
            ReviewProductData rpd = new ReviewProductData(productID, rating, displayName, comments, GetEmailAddress(displayName));
            SubmitReview(rpd);
        }

        private string GetEmailAddress(string displayName)
        {
            string query = "select Email from Person.Login where DisplayName = @DisplayName";

            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                queryStatus.Parameters.AddWithValue("@DisplayName", displayName);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    return $"{reader["Email"]}";
                }
            }
            return "";
        }

        private string GetCaseSensitiveUsername(string reviewName)
        {
            string query = "select DisplayName from Person.Login where DisplayName = @DisplayName";

            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DisplayName", reviewName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return $"{reader["DisplayName"]}";
                }
            }
            return "";
        }

        private string TrimUsername(string reviewName)
        {
            return reviewName.Substring(reviewName.IndexOf(":") + 2);
        }

        private void SubmitReview(ReviewProductData rpd)
        {
            string query = "insert into Production.ProductReview (ProductID, ReviewerName, ReviewDate, EmailAddress, Rating, Comments, ModifiedDate) " +
                "values (@ProductID, @ReviewerName, @ReviewDate, @EmailAddress, @Rating, @Comments, @ModifiedDate)";
            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
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
