using Adventure_Works_Desktop_App.Globals;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App.LoginPage.Backend
{
    public class LoginBackend
    {
        private Connection connection = new Connection();
        public AccountData accountData;

        public bool ValidateCredentials(string inputUsername, string inputPassword)
        {
            accountData = GetLoginDB(inputUsername, inputPassword);

            if (accountData.Username == null || accountData.Password == null)
            {
                return false;
            }

            if (!inputUsername.Equals(accountData.Username) || !inputPassword.Equals(accountData.Password))
            {
                return false;
            }

            accountData.DisplayName = GetDisplayName(inputUsername, inputPassword);

            return true; 
        }

        private string GetDisplayName(string username, string password)
        {
            string query = "select dbo.ufnGetDisplayName(@Username, @Password)";
            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            throw new Exception(Properties.LoginBackendResources.ExceptionCannotConnectToDB);
        }

        private AccountData GetLoginDB(string username, string password)
        {
            AccountData data = new AccountData();

            using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
            {
                conn.Open();
                
                using (SqlCommand cmd = new SqlCommand("dbo.uspGetUsernamePassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Username = reader["Username"].ToString();
                            data.Password = reader["Password"].ToString();
                        }
                    }
                }
            }

            return data;
        }
    }
}
