using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App
{
    internal class ProductData
    {
        private string productCategory;
        private string productSubCategory;
        private string productID;
        private string productNumber;
        private string productName;
        private string listPrice;
        private string standardCost;
        private string marginProfit;
        private string size;
        private string color;
        private string weight;
        private string description;
        private string cultureID; // this will dictate whether it is in the language chosen and will display as such

        public string ProductCategory
        {
            get { return productCategory; }
            set { productCategory = value; }
        }

        public string ProductSubCategory
        {
            get { return productSubCategory; }
            set { productSubCategory = value; }
        }

        public string ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        public string ProductNumber
        { 
            get { return productNumber; }
            set { productNumber = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string ListPrice
        {
            get { return listPrice; }
            set { listPrice = value; }
        }
        public string StandardCost
        {
            get { return standardCost; }
            set { standardCost = value; }
        }

        public string MarginProfit
        {
            get { return marginProfit; }
            set { marginProfit = value; }
        }

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string CultureID
        {
            get { return cultureID; }
            set { cultureID = value; }
        }

        public ProductData(string productCategory, string productSubCategory, string productID, string productNumber, string productName,
                           string listPrice, string standardCost, string marginProfit, string size, 
                           string color, string weight, string description, string cultureID)
        {
            this.productCategory = productCategory;
            this.productSubCategory = productSubCategory;
            this.productID = productID;
            this.productNumber = productNumber;
            this.productName = productName;
            this.listPrice = listPrice;
            this.standardCost = standardCost;
            this.marginProfit = marginProfit;
            this.size = size;
            this.color = color;
            this.weight = weight;
            this.description = description;
            this.cultureID = cultureID;
        }

        public ProductData()
        {

        }
        
    }
}
