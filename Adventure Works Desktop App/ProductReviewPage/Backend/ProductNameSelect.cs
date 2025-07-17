using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ProductNameSelect
    {
        Connection con = new Connection();

        // I am aware you can make this a generic function and class that just populates the combobox selected and pass a string that holds a query.
        // But this is just a practice project. (Similiar class - ProductLanguageSelect)
        public void PopulateComboBox(ComboBox comboBox, List<string> productIDs)
        {
            string query = "select p.ProductID, p.Name " +
                           "from Production.Product as p " +
                           "join Production.ProductSubcategory as ps on p.ProductSubcategoryID = ps.ProductSubcategoryID " +
                           "join Production.ProductCategory as pc on ps.ProductCategoryID = pc.ProductCategoryID " +
                           "where ListPrice <> 0.00";
            using (SqlConnection connect = new SqlConnection(con.ConnectionString))
            {
                connect.Open();
                SqlCommand queryStatus = new SqlCommand(query, connect);
                SqlDataReader reader = queryStatus.ExecuteReader();
                comboBox.Items.Clear();
                while (reader.Read())
                {
                    string language = $"{reader["Name"]}";
                    string productID = $"{reader["ProductID"]}";
                    productIDs.Add(productID);
                    comboBox.Items.Add(language);
                }
            }
        }
    }
}
