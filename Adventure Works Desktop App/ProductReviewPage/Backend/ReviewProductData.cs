using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ReviewProductData
    {
        Connection connection = new Connection();

        int productID, rating;
        string reviewerName, emailAddress, comments;
        DateTime reviewDate, modifiedDate;

        public int ProductID
        { 
            get { return productID; } set { productID = value; }
        }
        public int Rating
        {
            get { return rating; } set { rating = value; }
        }
        public string ReviewerName
        {
            get { return reviewerName; } set { reviewerName = value; }
        }
        public string EmailAddress
        {
            get { return emailAddress; } set { emailAddress = value; }
        }

        public string Comments
        {
            get { return comments; } set { comments = value; }
        }

        public DateTime ReviewDate
        {
            get { return reviewDate; }
        }

        public DateTime ModifiedDate
        {
            get { return modifiedDate; }
        }

        public ReviewProductData(string productID, int rating, string reviewName, string comments, string email)
        {
            this.productID = Int32.Parse(productID);
            this.rating = rating;
            this.reviewerName = reviewName;
            this.comments = comments;
            this.emailAddress = email;
            reviewDate = DateTime.Now;
            modifiedDate = DateTime.Now;
        }
    }
}
