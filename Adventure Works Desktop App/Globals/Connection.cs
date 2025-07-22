namespace Adventure_Works_Desktop_App
{
    public class Connection
    {
        private const string connectionString = "Server=LocalHost\\SQLDEVELOPER;Initial Catalog=AdventureWorks2017;Integrated Security=True;TrustServerCertificate=True;";
        public string ConnectionString
        { get { return connectionString; } }
    }
}
