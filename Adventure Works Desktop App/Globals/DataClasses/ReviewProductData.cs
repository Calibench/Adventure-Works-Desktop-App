using Adventure_Works_Desktop_App.Globals;
using System;

namespace Adventure_Works_Desktop_App.Globals.DataClasses
{
    public class ReviewProductData
    {
        public int ProductID
        { get; set; }
        public int Rating
        { get; set; }
        public string ReviewerName
        { get; set; }
        public string EmailAddress
        { get; set; }

        public string Comments
        { get; set; }

        public DateTime ReviewDate
        { get; }

        public DateTime ModifiedDate
        { get; }

        public ReviewProductData(string productID, int rating, string reviewName, string comments, string email)
        {
            ProductID = Int32.Parse(productID);
            Rating = rating;
            ReviewerName = reviewName;
            Comments = comments;
            EmailAddress = email;
            ReviewDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
    }
}
