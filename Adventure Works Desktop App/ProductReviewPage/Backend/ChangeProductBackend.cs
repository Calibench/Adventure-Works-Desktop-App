using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ChangeProductBackend
    {
        Connection con = new Connection();

        // I am aware you can make this a generic function and class that just populates the combobox selected and pass a string that holds a query.
        // But this is just a practice project. (Similiar class - ProductLanguageSelect)
        public void PopulateComboBox(ComboBox comboBox, List<string> productIDs)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(con.ConnectionString))
                {
                    connect.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.uspGetAllProducts", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBox.Items.Clear();
                            productIDs.Clear();
                            while (reader.Read())
                            {
                                string language = reader["Name"].ToString();
                                string productID = reader["ProductID"].ToString();
                                productIDs.Add(productID);
                                comboBox.Items.Add(language);
                            }
                            return;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in PopulateComboBox.", ex);
            }
        }
    }
}
