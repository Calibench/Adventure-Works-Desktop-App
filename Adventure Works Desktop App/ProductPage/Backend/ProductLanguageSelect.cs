using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App
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
            String query = "select Name " +
                           "from Production.Culture " +
                           "where CultureID <> '';";
            using (SqlConnection con = new SqlConnection(connect.ConnectionString))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                SqlDataReader reader = queryStatus.ExecuteReader();
                comboBox.Items.Clear();
                while (reader.Read())
                {
                    string language = $"{reader["Name"]}";
                    comboBox.Items.Add(language);
                }
            }
        }
    }
}
