using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.ProductPage.Backend
{
    internal class ProductLanguageBackend
    {
        public ProductLanguageBackend(ComboBox comboBox) 
        {
            RetrieveLanguages(comboBox);
        }

        /// <summary>
        /// Populates the specified <see cref="ComboBox"/> with a list of languages, excluding Spanish.
        /// </summary>
        /// <remarks>This method retrieves language names from a database using a stored procedure and
        /// adds them to the provided <see cref="ComboBox"/>. The list excludes Spanish due to the absence of product
        /// descriptions in that language.</remarks>
        /// <param name="comboBox">The <see cref="ComboBox"/> to populate with language names.</param>
        private void RetrieveLanguages(ComboBox comboBox)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString))
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
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database access failed in RetrieveLanguages.", ex);
            }
        }
    }
}
