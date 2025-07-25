using Adventure_Works_Desktop_App.Globals.DataClasses;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.EmployeePage.Backend
{
    /// <summary>
    /// This backend handle the employee info page that displays data about an employee to the user.
    /// </summary>
    internal class EmployeeInfoBackend
    {
        private readonly EmployeeDAL _dal = new EmployeeDAL();

        /// <summary>
        /// Gets employee ID's and populates them into a given combobox.
        /// </summary>
        /// <param name="comboBox">Combobox that is needs to be populated.</param>
        public void UpdateComoboBox(ComboBox comboBox)
        {
            string query = "dbo.uspGetAllEmployeeIDs",
                   columnHeader = "businessEntityID";
            _dal.DBGenComboBoxStoredProc(comboBox, query, columnHeader, null, null);
        }

        /// <summary>
        /// Given an employee ID, return data on the employee.
        /// </summary>
        /// <param name="businessEntityID">The employee ID.</param>
        /// <returns></returns>
        public EmployeeData GetData(string businessEntityID)
        {
            return _dal.DBGetEmployeeData(businessEntityID);
        }
    }
}
