using Adventure_Works_Desktop_App.Globals.DataClasses;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreDetailsEditBackend
    {
        private StoreDetailsDAL _dal = new StoreDetailsDAL();

        enum Check
        {
            notValid = -1,
            valid = 0
        }

        /// <summary>
        /// Updates the store address given address data.
        /// </summary>
        /// <param name="addressData">Address data to submit to the database and update.</param>
        /// <exception cref="InvalidOperationException">Database could not be connected to.</exception>
        public void UpdateStoreAddress(StoreAddressData addressData)
        {
            // CountryRegionCode
            string query = "select dbo.ufnGetCountryCode(@Name)";
            string countryRegionCode = _dal.GetGenCode(query, addressData.Country);

            if (countryRegionCode == null)
            {
                ValidIDCode((int)Check.notValid, "Country Region Code");
            }

            // StateProvinceID
            query = "select dbo.ufnGetStateProvinceID(@Name)";
            int stateProvinceId = _dal.GetGenID(query, addressData.StateProvinceName);
            ValidIDCode(stateProvinceId, "State Type ID");

            // AddressTypeID
            query = "select dbo.ufnGetAddressTypeID(@Name)";
            int addressTypeId = _dal.GetGenID(query, addressData.AddressType);
            ValidIDCode(addressTypeId, "Address Type ID");

            // get AddressID for this BusinessEntityID and AddressTypeID
            int addressId = _dal.GetAddressID(addressData, addressTypeId);
            ValidIDCode(addressId, "Address ID");

            _dal.UpdateAddress(addressData, stateProvinceId, addressId);
        }

        /// <summary>
        /// Validates the specified ID code and terminates the application if the validation fails.
        /// </summary>
        /// <remarks>
        /// If <paramref name="validCheck"/> equals <see cref="Check.notValid"/>, a message box
        /// is displayed with the provided <paramref name="id"/>, and the application is terminated.
        /// </remarks>
        /// <param name="validCheck">An integer representing the validation status. Must not be equal to <see cref="Check.notValid"/> for the
        /// application to continue running.</param>
        /// <param name="id">A string representing the identifier being validated. Used in the error message if validation fails.</param>
        private void ValidIDCode(int validCheck, string id)
        {
            if (validCheck == (int)Check.notValid)
            {
                MessageBox.Show($"Unable to Get the {id}, application will now close");
                Application.Exit();
            }
        }

        /// <summary>
        /// Updates the contact information for a store in the database.
        /// </summary>
        /// <remarks>This method updates the phone number type, phone number, email address, and email
        /// promotion status for a store contact identified by the provided business entity ID. The method throws an
        /// exception if the correct person ID cannot be found or if there is a failure in database access.</remarks>
        /// <param name="contactData">The contact data containing the updated information for the store contact, including business entity ID,
        /// phone number, email address, and promotion details.</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException">Thrown when database access fails during the update operation.</exception>
        public void UpdateStoreContact(StoreContactsData contactData)
        {
            // Get PersonID to then Get the PhoneNumber Table then update PhonenNumberTypeID with what the user gave
            // (only options are given on the person.phonenumbertype table)
            string query = "execute dbo.uspGetPersonIDs @BusinessEntityID = @Name";
            List<int> personID = new List<int>();
            List<StoreContactsData> contacts = new List<StoreContactsData>();
            _dal.GetPersonInformationID(query, personID, contacts, contactData);

            query = "select dbo.ufnGetPhoneNumberTypeID(@Name)";
            int phoneNumberTypeID = _dal.GetGenID(query, contactData.PhoneNumberType);

            // now update the phonenumbertype
            // ensure you are modifying the right one by using the contacts list and comparing it to the contactdata that passed

            int index = 0;
            int correctPersonID = (int)Check.notValid;
            try
            {
                foreach (StoreContactsData data in contacts)
                {
                    if (data.Title.Equals(contactData.Title) && data.FirstName.Equals(contactData.FirstName) &&
                        data.MiddleName.Equals(contactData.MiddleName) && data.LastName.Equals(contactData.LastName))
                    {
                        correctPersonID = personID[index];
                    }
                    index++;
                }
                if (correctPersonID == (int)Check.notValid)
                {
                    MessageBox.Show("Not able to find person id, returning.");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("COULD NOT FIND CORRECT PERSONID", ex);
            }

            // GenPersonUpdateOneParam(SqlConnection conn, string query, string param1, int personID, string columnName)
            _dal.GenPersonUpdateOneParam("dbo.uspUpdatePersonPhoneNumberTypeID", $"{phoneNumberTypeID}", correctPersonID, "@ID");

            _dal.GenPersonUpdateOneParam("dbo.uspUpdatePersonPhoneNumber", contactData.PhoneNumber, correctPersonID, "@PhoneNumber");

            //execute uspUpdatePersonEmailAddress @PersonID = @PID, @EmailAddress = @EA
            _dal.GenPersonUpdateOneParam("dbo.uspUpdatePersonEmailAddress", contactData.EmailAddress, correctPersonID, "@EmailAddress");

            _dal.GenPersonUpdateOneParam("dbo.uspUpdatePersonEmailPromotion", contactData.EamilPromotion, correctPersonID, "@EmailPromotion");
        }

        /// <summary>
        /// Determines whether the specified phone number type is valid by checking against the database.
        /// </summary>
        /// <remarks>This method queries the database to verify the existence of the provided phone number
        /// type. Ensure that the database connection string is correctly configured in the application
        /// settings.</remarks>
        /// <param name="phoneNumberType">The phone number type to validate.</param>
        /// <returns><see langword="true"/> if the specified phone number type exists in the database; otherwise, <see
        /// langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if there is a failure accessing the database.</exception>
        public bool ValidPhoneNumberType(string phoneNumberType)
        {
            return _dal.ValidPhoneNumberType(phoneNumberType);
        }
    }
}
