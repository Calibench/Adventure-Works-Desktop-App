namespace Adventure_Works_Desktop_App.Globals.DataClasses
{
    /// <summary>
    /// Employee Details is used to store employee data. It is mainly used in the Employee Page. 
    /// </summary>
    public class EmployeeData
    {
        public string BusinessEntityID
        { get; set; }
        public string FirstName
        { get; set; }
        public string MiddleName
        { get; set; }
        public string LastName
        { get; set; }
        public string JobTitle
        { get; set; }
        public string BirthDate
        { get; set; }
        public string MaritalStatus
        { get; set; }
        public string Gender
        { get; set; }
        public string HireDate
        { get; set; }
        public string VacationHours
        { get; set; }
        public string SickLeaveHours
        { get; set; }
        public string DepartmentName
        { get; set; }
        public string DepartmentGroupName
        { get; set; }
        public string ShiftName
        { get; set; }
        public string YearlySalary
        { get; set; }

        /// <summary>
        /// Empty employee data constructor
        /// </summary>
        public EmployeeData()
        { 
        
        }

        /// <summary>
        /// A constructor to ensure all properties of EmployeeData is filled. 
        /// </summary>
        /// <param name="businessEntityID">The unique ID for this employee</param>
        /// <param name="firstName">The first name of this employee</param>
        /// <param name="middleName">The middle name of this employee</param>
        /// <param name="lastName">The last name of this employee</param>
        /// <param name="jobTitle">The job title of this employee</param>
        /// <param name="birthDate">The birth date of this employee</param>
        /// <param name="maritalStatus">The martial status of this employee</param>
        /// <param name="gender">The gender of this employee</param>
        /// <param name="hireDate">The hire date of this employee</param>
        /// <param name="vacationHours">The vacation hours of this employee</param>
        /// <param name="sickLeaveHours">The sick leave hours of this employee</param>
        /// <param name="departmentName">The department name of this employee</param>
        /// <param name="departmentGroupName">The department group name of this employee</param>
        /// <param name="shiftName">The shift name of this employee</param>
        /// <param name="yearlySalary">The yearly salary of this employee</param>
        public EmployeeData(string businessEntityID, 
                               string firstName, string middleName, string lastName, 
                               string jobTitle, string birthDate, string maritalStatus, 
                               string gender, string hireDate, string vacationHours, 
                               string sickLeaveHours, string departmentName, string departmentGroupName, 
                               string shiftName, string yearlySalary) 
        {
            BusinessEntityID = businessEntityID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            JobTitle = jobTitle;
            BirthDate = birthDate;
            MaritalStatus = maritalStatus;
            Gender = gender;
            HireDate = hireDate;
            VacationHours = vacationHours;
            SickLeaveHours = sickLeaveHours;
            DepartmentName = departmentName;
            DepartmentGroupName = departmentGroupName;
            ShiftName = shiftName;
            YearlySalary = yearlySalary;
        }
    }
}
