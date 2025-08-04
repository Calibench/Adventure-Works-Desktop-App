using Adventure_Works_Desktop_App.Globals.DataClasses;
using System.Collections.Generic;
namespace Adventure_Works_Desktop_App.StoreDetailsPage.Backend
{
    internal class StoreDetailsBackend
    {
        public List<StoreAddressData> addressData;
        public List<StoreContactsData> contactsData;
        public List<StoreDemographicsData> demographicsData;

        public List<StoreAddressData> addressDataSingle;
        public List<StoreContactsData> contactsDataSingle;
        public List<StoreDemographicsData> demographicsDataSingle;

        private string id;
        private StoreDetailsDAL _dal = new StoreDetailsDAL();

        public StoreDetailsBackend()
        {
            addressData = new List<StoreAddressData>();
            contactsData = new List<StoreContactsData>();
            demographicsData = new List<StoreDemographicsData>();
        }

        public StoreDetailsBackend(string id)
        {
            addressDataSingle = new List<StoreAddressData>();
            contactsDataSingle = new List<StoreContactsData>();
            demographicsDataSingle = new List<StoreDemographicsData>();
            this.id = id;
        }

        // Used in Details form to get specifics (through ID)
        public void GetSpecificData()
        {
            _dal.GetSpecificData(id, addressDataSingle, contactsDataSingle, demographicsDataSingle);
        }

        public void SortedByStoreName(string userSearchText)
        {
            string query = "dbo.SearchStoreName";
            string param = "@StoreName";
            string value = userSearchText;
            _dal.SortedByStore(query, param, value, addressData, contactsData, demographicsData);
        }

        public void SortedByStoreCity(string userSearchText)
        {
            string query = "dbo.SearchStoreCity";
            string param = "@City";
            string value = userSearchText;
            _dal.SortedByStore(query, param, value, addressData, contactsData, demographicsData);
        }

        public void SortedByStoreCountry(string userSearchText)
        {
            string query = "dbo.SearchStoreCountry";
            string param = "@Country";
            string value = userSearchText;
            _dal.SortedByStore(query, param, value, addressData, contactsData, demographicsData);
        }

        public void SortedByStoreYear(string userSearchText)
        {
            string query = "dbo.SearchStoreYear";
            string param = "@Year";
            string value = userSearchText;
            _dal.SortedByStore(query, param, value, addressData, contactsData, demographicsData);
        }

        public void GetAllStoreDetailList()
        {
            _dal.GetAllAddressData(addressData);
            _dal.GetAllContactsData(contactsData);
            _dal.GetAllDemographicsData(demographicsData);
        }
    }
}
