using Adventure_Works_Desktop_App.StoreDetailsPage.Backend;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Adventure_Works_Desktop_App.StoreDetailsPage.Frontend
{
    public partial class StoreDetailsForm : Form
    {
        private string username;
        private StoreDetailsBackend backend;
        private bool editor = false;

        public bool backButtonPressed = false;
        public bool saveDataPressed = false;

        private string addressTypeOriginal = "";
        private string contactTypeOriginal = "";

        enum AddressChoice
        {
            Main_Office,
            Shipping,
            None
        }

        enum ContactChoice
        {
            Owner,
            Purchasing_Agent,
            Purchasing_Manager,
            None
        }

        public StoreDetailsForm(string username, string id, bool editor)
        {
            InitializeComponent();
            this.username = username;
            backend = new StoreDetailsBackend(id);
            this.editor = editor;
        }

        private void LoadInitialForm(object sender, EventArgs e)
        {
            usernameLabel.Text = "Logged in: " + username;
            buttonToolTip.SetToolTip(saveDataButton, "Save changes for this section only");
            buttonToolTip.SetToolTip(changeButton, "Show data relating to the selected type");
            saveDataButton.Visible = false;

            backend.GetSpecificData();
            bool initialLoad = true;
            PopulateData(initialLoad, AddressChoice.None, ContactChoice.None);

            if (editor)
            {
                AllowEditing();
                addressTypeOriginal = addressTypeComboBox.Text;
                contactTypeOriginal = contactTypeComboBox.Text;
            }
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            backButtonPressed = true;
            this.Close();
        }

        private void PopulateData(bool initialLoad, AddressChoice addChoice, ContactChoice conChoice)
        {
            // update text/fields
            if (initialLoad)
            {
                // This is only done for initial load to ensure proper load
                int firstLoad = 0;
                UpdateAddress(firstLoad);
                UpdateContact(firstLoad);
                UpdateDemographics(firstLoad);
                return;
            }
            if (!(addChoice == AddressChoice.None))
            {
                switch (addressTypeComboBox.SelectedIndex)
                {
                    case (int)AddressChoice.Main_Office:
                        UpdateAddress((int)AddressChoice.Main_Office);
                        return;
                    case (int)AddressChoice.Shipping:
                        UpdateAddress((int)AddressChoice.Shipping);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException("ERROR WITH ADDRESS CHOICE: ER-72-83 " 
                                                              + addressTypeComboBox.SelectedIndex + " " + addressTypeComboBox.Text);
                }
            }
            if (!(conChoice == ContactChoice.None))
            {
                switch (contactTypeComboBox.SelectedIndex)
                {
                    case (int)ContactChoice.Owner: // this does not necessarily mean that the index is owner, however it will be if it exists
                        UpdateContact((int)ContactChoice.Owner);
                        return;
                    case (int)ContactChoice.Purchasing_Agent: // same with this, if owner exists and this does, then this will be the spot
                        UpdateContact((int)ContactChoice.Purchasing_Agent);
                        return;
                    case (int)ContactChoice.Purchasing_Manager: // if the last two exist then this will be the final one to show
                        UpdateContact((int)ContactChoice.Purchasing_Manager);
                        return;
                    default:
                        throw new ArgumentOutOfRangeException("ERROR WITH CONTACT CHOICE: ER-86-100 " 
                                                              + contactTypeComboBox.SelectedIndex + " " + contactTypeComboBox.Text);
                }
            }
        }

        private void UpdateAddress(int index)
        {
            addressBusinessIDTextBox.Text = backend.addressDataSingle[index].BusinessEntityID;
            addressStoreNameTextBox.Text = backend.addressDataSingle[index].StoreName;
            addressTypeComboBox.Items.Clear();
            foreach (StoreAddressData data in backend.addressDataSingle)
            {
                if (addressTypeComboBox.Items.Contains(data.AddressType))
                {
                    continue;
                }
                addressTypeComboBox.Items.Add(data.AddressType);
            }
            addressTypeComboBox.SelectedIndex = index;
            addressTextBox.Text = backend.addressDataSingle[index].AddressLine1;
            altAddressTextBox.Text = backend.addressDataSingle[index].AddressLine2;
            cityTextBox.Text = backend.addressDataSingle[index].City;
            stateTextBox.Text = backend.addressDataSingle[index].StateProvinceName;
            postalCodeTextBox.Text = backend.addressDataSingle[index].PostalCode;
            countryTextBox.Text = backend.addressDataSingle[index].Country;
        }

        private void UpdateContact(int index)
        {
            contactBusinessIDTextBox.Text = backend.contactsDataSingle[index].BusinessEntityID;
            contactStoreNameTextBox.Text = backend.contactsDataSingle[index].StoreName;
            contactTypeComboBox.Items.Clear();
            foreach (StoreContactsData data in backend.contactsDataSingle)
            {
                if (contactTypeComboBox.Items.Contains(data.ContactType))
                {
                    continue;
                }
                contactTypeComboBox.Items.Add(data.ContactType);
            }
            contactTypeComboBox.SelectedIndex = index;
            titleTextBox.Text = backend.contactsDataSingle[index].Title;
            firstNameTextBox.Text = backend.contactsDataSingle[index].FirstName;
            middleNameTextBox.Text = backend.contactsDataSingle[index].MiddleName;
            lastNameTextBox.Text = backend.contactsDataSingle[index].LastName;
            suffixTextBox.Text = backend.contactsDataSingle[index].Suffix;
            phoneNumberTextBox.Text = backend.contactsDataSingle[index].PhoneNumber;
            phoneNumberTypeTextBox.Text = backend.contactsDataSingle[index].PhoneNumberType;
            emailAddressTextBox.Text = backend.contactsDataSingle[index].EmailAddress;
            emailPromotionTextBox.Text = backend.contactsDataSingle[index].EamilPromotion;
        }

        private void UpdateDemographics(int index)
        {
            demographicsBusinessIDTextBox.Text = backend.demographicsDataSingle[index].BusinessEntityID;
            demographicsStoreNameTextBox.Text = backend.demographicsDataSingle[index].StoreName;
            annualSalesTextBox.Text = backend.demographicsDataSingle[index].AnnualSales;
            annualRevenueTextBox.Text = backend.demographicsDataSingle[index].AnnualRevenue;
            bankNameTextBox.Text = backend.demographicsDataSingle[index].BankName;
            businessTypeTextBox.Text = backend.demographicsDataSingle[index].BusinessType;
            yearOpenTextBox.Text = backend.demographicsDataSingle[index].YearOpened;
            specialtyTextBox.Text = backend.demographicsDataSingle[index].Specialty;
            squareFeetTextBox.Text = backend.demographicsDataSingle[index].SquareFeet;
            brandsTextBox.Text = backend.demographicsDataSingle[index].Brands;
            internetTextBox.Text = backend.demographicsDataSingle[index].Internet;
            numOfEmployeesTextBox.Text = backend.demographicsDataSingle[index].NumberOfEmployees;
        }

        private void UpdateDataFromAddressType()
        {
            AddressChoice addChoice = AddressChoice.None;
            switch (addressTypeComboBox.SelectedIndex)
            {
                case (int)AddressChoice.Main_Office:
                    addChoice = AddressChoice.Main_Office;
                    break;
                case (int)AddressChoice.Shipping:
                    addChoice = AddressChoice.Shipping;
                    break;
                default:
                    addChoice = AddressChoice.None;
                    break;
            }

            PopulateData(false, addChoice, ContactChoice.None);
        }

        private void UpdateDataFromContactType()
        {
            ContactChoice conChoice = ContactChoice.None;
            switch (contactTypeComboBox.SelectedIndex)
            {
                case (int)ContactChoice.Owner:
                    conChoice = ContactChoice.Owner;
                    break;
                case (int)ContactChoice.Purchasing_Agent:
                    conChoice = ContactChoice.Purchasing_Agent;
                    break;
                case (int)ContactChoice.Purchasing_Manager:
                    conChoice = ContactChoice.Purchasing_Manager;
                    break;
                default:
                    conChoice = ContactChoice.None;
                    break;
            }

            PopulateData(false, AddressChoice.None, conChoice);
        }

        enum SelectedTab
        { 
            AddressTab,
            ContactTab,
            DemograhpicTab
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            switch (detailsTabControl.SelectedIndex)
            {
                case (int)SelectedTab.AddressTab:
                    UpdateDataFromAddressType();
                    addressTypeOriginal = addressTypeComboBox.Text;
                    break;
                case (int)SelectedTab.ContactTab:
                    UpdateDataFromContactType();
                    contactTypeOriginal = contactTypeComboBox.Text;
                    break;
                case (int)SelectedTab.DemograhpicTab:
                    break;
            }
            
        }

        // editting data section
        private void AllowEditing()
        {
            // ensure the save button is allowed
            saveDataButton.Visible = true;
            
            // change read-only fields to be able to write 
            // (Address Tab)
            addressTextBox.ReadOnly = false;
            altAddressTextBox.ReadOnly = false;
            cityTextBox.ReadOnly = false;
            stateTextBox.ReadOnly = false;
            postalCodeTextBox.ReadOnly = false;
            countryTextBox.ReadOnly = false;

            // (Contact Tab)
            phoneNumberTextBox.ReadOnly = false;
            phoneNumberTypeTextBox.ReadOnly = false;
            emailAddressTextBox.ReadOnly = false;
            emailPromotionTextBox.ReadOnly = false;
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            StoreDetailsEditBackend editBackend = new StoreDetailsEditBackend();
            if (!ValidateFields(editBackend))
            {
                return;
            }

            StoreAddressData addressData = PackageAddressData();
            StoreContactsData contactsData = PackageContactData();

            editBackend.UpdateStoreAddress(addressData);
            editBackend.UpdateStoreContact(contactsData);

            saveDataPressed = true;

            MessageBox.Show("Saved");
        }

        private StoreAddressData PackageAddressData()
        { 
            StoreAddressData temp = new StoreAddressData();

            temp.BusinessEntityID = addressBusinessIDTextBox.Text;
            temp.StoreName = addressStoreNameTextBox.Text;
            temp.AddressType = addressTypeComboBox.Text;
            temp.AddressLine1 = addressTextBox.Text;
            temp.AddressLine2 = altAddressTextBox.Text;
            temp.City = cityTextBox.Text;
            temp.StateProvinceName = stateTextBox.Text;
            temp.PostalCode = postalCodeTextBox.Text;
            temp.Country = countryTextBox.Text;

            return temp;
        }

        private StoreContactsData PackageContactData() 
        {
            StoreContactsData temp = new StoreContactsData();

            temp.BusinessEntityID = contactBusinessIDTextBox.Text;
            temp.StoreName = contactStoreNameTextBox.Text;
            temp.ContactType = contactTypeComboBox.Text;
            temp.Title = titleTextBox.Text;
            temp.FirstName = firstNameTextBox.Text;
            temp.MiddleName = middleNameTextBox.Text;
            temp.LastName = lastNameTextBox.Text;
            temp.Suffix = suffixTextBox.Text;
            temp.PhoneNumber = phoneNumberTextBox.Text;
            temp.PhoneNumberType = phoneNumberTypeTextBox.Text;
            temp.EmailAddress = emailAddressTextBox.Text;
            temp.EamilPromotion = emailPromotionTextBox.Text;

            return temp;
        }

        private bool ValidateFields(StoreDetailsEditBackend editBackend)
        {
            if (!ValidateAddressFields())
            { 
                return false;
            }
            if (!ValidateContactFields(editBackend))
            {
                return false;
            } 

            return true;
        }

        private bool ValidateAddressFields()
        {
            /* Fields to check:
            * AddressType (To ensure that it's not randomly entered as updating is through the current text)
            * ^ if this is change from og but change button not hit then deny save ^
            * Address
            * Alternate Address
            * City
            * State/Province
            * Postal Code
            * Country
            */

            // get the original address data for the selected index
            int index = addressTypeComboBox.SelectedIndex;
            if (index < 0 || index >= backend.addressDataSingle.Count)
            {
                MessageBox.Show("Please select a valid address type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // AddressType validation: match the selected type, not random text
            if (addressTypeComboBox.Text != addressTypeOriginal)
            {
                MessageBox.Show("Address Type does not match the original. Please use the change button to update.", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // required fields: AddressLine1, City, StateProvinceName, PostalCode, Country
            if (string.IsNullOrWhiteSpace(addressTextBox.Text))
            {
                MessageBox.Show("Address Line 1 is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cityTextBox.Text))
            {
                MessageBox.Show("City is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(stateTextBox.Text))
            {
                MessageBox.Show("State/Province is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(postalCodeTextBox.Text))
            {
                MessageBox.Show("Postal Code is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(countryTextBox.Text))
            {
                MessageBox.Show("Country is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // simple Postal code format
            if (!System.Text.RegularExpressions.Regex.IsMatch(postalCodeTextBox.Text, @"^[A-Za-z0-9\- ]+$"))
            {
                MessageBox.Show("Postal Code format is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateContactFields(StoreDetailsEditBackend editBackend)
        {
            /* Fields to check:
             * ContactType (To ensure that it's not randomly entered as updating is throught he current text) 
             * ^ if this is change from og but change button not hit then deny save ^
             * Phone Number
             * Phone Number Type
             * Email Address
             * Email Promotion
             */

            // get the original contact data for the selected index
            int index = contactTypeComboBox.SelectedIndex;
            if (index < 0 || index >= backend.contactsDataSingle.Count)
            {
                MessageBox.Show("Please select a valid contact type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ContactType validation: match the selected type, not random text
            if (contactTypeComboBox.Text != contactTypeOriginal)
            {
                MessageBox.Show("Contact Type does not match the original. Please use the change button to update the contact details.", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // required fields: PhoneNumber, PhoneNumberType, EmailAddress, EmailPromotion
            if (string.IsNullOrWhiteSpace(phoneNumberTextBox.Text))
            {
                MessageBox.Show("Phone Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(phoneNumberTypeTextBox.Text))
            {
                MessageBox.Show("Phone Number Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!editBackend.ValidPhoneNumberType(phoneNumberTypeTextBox.Text))
            {
                MessageBox.Show("Phone Number Type does not exist. Please try one of the following:\nWork, Cell, Home", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (string.IsNullOrWhiteSpace(emailAddressTextBox.Text))
            {
                MessageBox.Show("Email Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(emailPromotionTextBox.Text))
            {
                MessageBox.Show("Email Promotion is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // basic phone number format check (digits, spaces, dashes, parentheses)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumberTextBox.Text, @"^[\d\s\-\(\)\+]+$"))
            {
                MessageBox.Show("Phone Number format is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // basic email format check
            if (!System.Text.RegularExpressions.Regex.IsMatch(emailAddressTextBox.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email Address format is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
