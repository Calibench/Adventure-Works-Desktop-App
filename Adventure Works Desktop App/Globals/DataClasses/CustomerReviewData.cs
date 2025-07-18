namespace Adventure_Works_Desktop_App.Globals.DataClasses
{
    /// <summary>
    /// Customer Review Details is used to store review data. It is mainly used in the Products Pages. 
    /// </summary>
    public class CustomerReviewData
    {
        public string ProductID
        { get; set; }

        public string CustomerName
        { get; set; }

        public string Date
        { get; set; }

        public int Rating
        { get; set; }

        public string Comment
        { get; set; }

        public CustomerReviewData(string productID, string customerName, string date, int rating, string comment) 
        {
            ProductID = productID;
            CustomerName = customerName;
            Date = date;
            Rating = rating;
            Comment = comment;
        }
    }
}
