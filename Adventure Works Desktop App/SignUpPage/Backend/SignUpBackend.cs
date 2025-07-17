using Adventure_Works_Desktop_App.Globals;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.SignUpPage.Backend
{
    public class SignUpBackend
    {
        Connection connection = new Connection();

        public bool CheckUnique(AccountData data)
        {
            bool unique = true;
            string tempusername = "";
            string query = $"execute CheckUsername @Username = {data.Username}";
            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                SqlCommand queryStatus = new SqlCommand(query, con);
                SqlDataReader reader = queryStatus.ExecuteReader();
                while (reader.Read())
                {
                    tempusername = $"{reader["Username"]}";
                }
            }

            if (tempusername.Length >= 1)
            {
                unique = false;
            }
            return unique;
        }

        public void SignUp(AccountData data)
        {
            string query = $"insert into Person.Login (FirstName, LastName, Username, Password, DisplayName, Email) " +
                $"values (@FirstName, @LastName, @Username, @Password, @DisplayName, @Email)";
            using (SqlConnection con = new SqlConnection(connection.GetConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@Username", data.Username);
                    cmd.Parameters.AddWithValue("@Password", data.Password);
                    cmd.Parameters.AddWithValue("@DisplayName", data.DisplayName);
                    cmd.Parameters.AddWithValue("@Email", data.Email);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
