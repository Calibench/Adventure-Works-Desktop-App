using System.Collections.Generic;

namespace Adventure_Works_Desktop_App.ProductPage.Backend
{
    internal class ProductLanguageBackend
    {
        private readonly ProductDAL _dal = new ProductDAL();

        public List<string> RetrieveLanguages()
        {
            return _dal.DBRetrieveLanguages();
        }
    }
}
