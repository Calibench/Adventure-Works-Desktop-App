namespace Adventure_Works_Desktop_App.Globals.DataClasses
{
    public class ProductData
    {
        public string ProductCategory
        { get; set; }

        public string ProductSubCategory
        { get; set; }

        public string ProductID
        { get; set; }

        public string ProductNumber
        { get; set; }

        public string ProductName
        { get; set; }

        public string ListPrice
        { get; set; }

        public string StandardCost
        { get; set; }

        public string MarginProfit
        { get; set; }

        public string Size
        { get; set; }

        public string Color
        { get; set; }

        public string Weight
        { get; set; }

        public string Description
        { get; set; }

        public string CultureID // this will dictate whether it is in the language chosen and will display as such
        { get; set; }

        public ProductData(string productCategory, string productSubCategory, string productID, string productNumber, string productName,
                           string listPrice, string standardCost, string marginProfit, string size, 
                           string color, string weight, string description, string cultureID)
        {
            ProductCategory = productCategory;
            ProductSubCategory = productSubCategory;
            ProductID = productID;
            ProductNumber = productNumber;
            ProductName = productName;
            ListPrice = listPrice;
            StandardCost = standardCost;
            MarginProfit = marginProfit;
            Size = size;
            Color = color;
            Weight = weight;
            Description = description;
            CultureID = cultureID;
        }

        public ProductData()
        {

        }
        
    }
}
