using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Backend
{
    internal class RegionData
    {
        public string TerritoryID
        { get; set; }
        public string RegionName
        { get; set; }
        public string RegionCode
        { get; set; }
        public string Continent
        {  get; set; }
        public string TotalSalesYTD
        { get; set; }
        public string TotalSalesLY
        { get; set; }

        public RegionData()
        { 
            // used for ease of initialization
        }
    }
}
