using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.BackEnd
{
    internal class ProductLanguageSelect
    {
        Connection connect = new Connection();
        public ProductLanguageSelect(ComboBox comboBox) 
        {
            RetrieveLanguages(comboBox);
        }

        private void RetrieveLanguages(ComboBox comboBox)
        {
            // no spanish due to no product description about it.
            using (SqlConnection con = new SqlConnection(connect.ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.uspGetAllNotSpanishLanguages", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader["Name"].ToString());
                        }
                    }
                }   
            }
        }
    }
}
