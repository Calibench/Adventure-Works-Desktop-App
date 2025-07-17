using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Backend
{
    internal class SalesPersonData
    {
        public string BusinessEntityID
        {get;set;}
        public string SalesQuota
        {get;set;}
        public string Bonus
        {get;set;}
        public string CommissionPct
        {get;set;}
        public string SalesYTD
        {get;set;}
        public string SalesLY
        {get;set;}
        public string FirstName
        {get;set;}
        public string LastName
        {get;set;}
        public RegionData RegionData
        {get;set;}

        public SalesPersonData()
        {
            // used for ease of initialization
        }
    }
}
