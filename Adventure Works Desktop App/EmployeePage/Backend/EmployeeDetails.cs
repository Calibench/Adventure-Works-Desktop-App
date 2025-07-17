using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_Works_Desktop_App
{
    public class EmployeeDetails
    {
        private string businessEntityID;
        private string firstName;
        private string middleName;
        private string lastName;
        private string jobTitle;
        private string birthDate;
        private string maritalStatus;
        private string gender;
        private string hireDate;
        private string vacationHours;
        private string sickLeaveHours;
        private string departmentName;
        private string departmentGroupName;
        private string shiftName;
        private string yearlySalary;

        public EmployeeDetails()
        { 
        
        }
        public EmployeeDetails(string businessEntityID, 
                               string firstName, string middleName, string lastName, 
                               string jobTitle, string birthDate, string maritalStatus, 
                               string gender, string hireDate, string vacationHours, 
                               string sickLeaveHours, string departmentName, string departmentGroupName, 
                               string shiftName, string yearlySalary) 
        {
            this.businessEntityID = businessEntityID;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.jobTitle = jobTitle;
            this.birthDate = birthDate;
            this.maritalStatus = maritalStatus;
            this.gender = gender;
            this.hireDate = hireDate;
            this.vacationHours = vacationHours;
            this.sickLeaveHours = sickLeaveHours;
            this.departmentName = departmentName;
            this.departmentGroupName = departmentGroupName;
            this.shiftName = shiftName;
            this.yearlySalary = yearlySalary;
        }

        public bool SuccessfullyGetDetails()
        {
            if (this.departmentName != null)
            {
                return true;
            }
            return false;
        }

        public string GetBusinessEntityID() 
        {
            return this.businessEntityID;
        }
        public string GetFirstName() 
        {
            return this.firstName;
        }
        public string GetMiddleName() 
        {
            return this.middleName;
        }
        public string GetLastName() 
        {
            return this.lastName;
        }
        public string GetJobTitle() 
        {
            return this.jobTitle;
        }
        public string GetBirthDate() 
        {
            return this.birthDate;
        }
        public string GetMaritalStatus() 
        {
            return this.maritalStatus;
        }
        public string GetGender() 
        {
            return this.gender;
        }
        public string GetHireDate() 
        {
            return this.hireDate;
        }
        public string GetVacationHours() 
        {
            return this.vacationHours;
        }
        public string GetSickLeaveHours() 
        {
            return this.sickLeaveHours;
        }
        public string GetDepartmentName() 
        {
            return this.departmentName;
        }
        public string GetDepartmentGroupName() 
        {
            return this.departmentGroupName;
        }
        public string GetShiftName() 
        {
            return this.shiftName;
        }
        public string GetYearlySalary() 
        {
            return this.yearlySalary;
        }

    }
}
