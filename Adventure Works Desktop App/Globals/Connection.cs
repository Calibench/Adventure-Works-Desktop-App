using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App
{
    internal class Connection
    {
        private string connectionString = "Server=LocalHost\\SQLDEVELOPER;Initial Catalog=AdventureWorks2017;Integrated Security=True;TrustServerCertificate=True;";
        
        public string GetConnectionString()
        { 
            return connectionString;
        }
    }
}
