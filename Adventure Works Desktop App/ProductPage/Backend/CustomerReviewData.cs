using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App
{
    internal class CustomerReviewData
    {
        private string productID;
        private string customerName;
        private string date; // yyyy-MM-dd
        private int rating; // out of 5
        private string comment;

        public string ProductID
        {
            get { return productID; } 
            set { productID = value; } 
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public CustomerReviewData(string productID, string customerName, string date, int rating, string comment) 
        {
            this.productID = productID;
            this.customerName = customerName;
            this.date = date;
            this.rating = rating;
            this.comment = comment;
        }
    }
}
