using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreAddressData
    {
        public string BusinessEntityID { get; set; }
        public string StoreName { get; set; }
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public StoreAddressData() 
        { 
            
        }
    }
}
