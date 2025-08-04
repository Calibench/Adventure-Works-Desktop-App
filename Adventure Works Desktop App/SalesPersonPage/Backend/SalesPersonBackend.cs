using Adventure_Works_Desktop_App.Globals.DataClasses;
using System.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;

namespace Adventure_Works_Desktop_App.SalesPersonPage.Backend
{
    internal class SalesPersonBackend
    {
        SalesPersonDAL _dal = new SalesPersonDAL();

        private string id;
        public string Id
        { 
            get { return id; }
        }

        public bool ValidateID(string id)
        {
            return Val(SearchDBForID(id));
        }

        public bool ValidateName(string name)
        {
            return Val(SearchDBForName(name));
        }

        private string SearchDBForID(string id)
        {
            return GenQueryScalar("select dbo.ufnValidateSalesPersonID(@ID)", "@ID", id);
        }

        private string SearchDBForName(string name)
        {
            return GenQueryScalar("select dbo.ufnValidateSalesPersonIDWithName(@Name)", "@Name", name);
        }

        /// <summary>
        /// Retrieves sales person data for a specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sales person whose data is to be retrieved. Cannot be null or empty.</param>
        /// <returns>A <see cref="SalesPersonData"/> object containing detailed information about the sales person, including
        /// personal details and sales performance metrics. Returns an empty <see cref="SalesPersonData"/> object if no
        /// data is found for the specified identifier.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public SalesPersonData GetSalesPersonData(string id)
        {
            return _dal.GetSalesPersonData(id);
        }

        // Region Specific
        public bool RegionIsValid(string name)
        {
            return Val(SearchDbForRegionName(name));
        }

        public string SearchDbForRegionName(string name)
        {
            return GenQueryScalar("select dbo.ufnValidateRegionName(@Name)", "@Name", name);
        }

        /// <summary>
        /// Retrieves region data for a specified region name from the database.
        /// </summary>
        /// <remarks>This method executes a stored procedure to fetch region data. Ensure that the
        /// database connection  string is correctly configured and that the stored procedure 'uspGetRegionData' is
        /// available in the database.</remarks>
        /// <param name="name">The name of the region for which to retrieve data. Cannot be null or empty.</param>
        /// <returns>A <see cref="RegionData"/> object containing details about the specified region,  including the region name,
        /// region code, territory ID, continent, total sales year-to-date,  and total sales from the last year.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public RegionData GetRegionData(string name)
        { 
            return _dal.GetRegionData(name);
        }

        // Helper Methods
        /// <summary>
        /// Validates the specified string and assigns it to the identifier.
        /// </summary>
        /// <param name="temp">The string to validate and assign. Must not be null and must have a length of at least 1.</param>
        /// <returns><see langword="true"/> if the string is valid and assigned; otherwise, <see langword="false"/>.</returns>
        private bool Val(string temp)
        {
            id = temp;
            if (temp != null && temp.Length >= 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Executes a SQL query and returns the first column of the first row in the result set as a string.
        /// </summary>
        /// <param name="query">The SQL query to execute. Must be a valid SQL command that returns a scalar value.</param>
        /// <param name="param">The name of the parameter to be added to the SQL command. This should match a parameter placeholder in the
        /// query.</param>
        /// <param name="data">The value to be assigned to the parameter specified by <paramref name="param"/>.</param>
        /// <returns>The first column of the first row in the result set as a string, or <see langword="null"/> if the result set
        /// is empty.</returns>
        /// <exception cref="InvalidOperationException">Thrown if database access fails during the execution of the query.</exception>
        private string GenQueryScalar(string query, string param, string data)
        {
            return _dal.GenQueryScalar(query, param, data);
        }
    }
}
