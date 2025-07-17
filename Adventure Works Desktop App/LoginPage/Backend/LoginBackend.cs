using Adventure_Works_Desktop_App.Globals;
using System.Data.SqlClient;

namespace Adventure_Works_Desktop_App
{
    public class LoginBackend
    {
        private Connection connection = new Connection();
        public AccountData accountData;

        public bool ValidateCredentials(string inputUsername, string inputPassword)
        {
            accountData = GetLoginDB(inputUsername, inputPassword);

            if (accountData.Username.Length <= 0 || accountData == null)
            {
                return false;
            }

            if (!inputUsername.Equals(accountData.Username) || !inputPassword.Equals(accountData.Password))
            {
                return false;
            }
            
            string displayName = GetDisplayName(inputUsername, inputPassword);
            if (displayName.Equals("ERROR"))
            {
                return false;
            }

            accountData.DisplayName = displayName;

            return true; 
        }

        private string GetDisplayName(string username, string password)
        {
            string query = $"select dbo.ufnGetDisplayName(@Username, @Password)";
            using (SqlConnection conn = new SqlConnection(connection.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return $"{result}";
                    }
                }
            }
            return "ERROR";
        }

        private AccountData GetLoginDB(string username, string password)
        {
            AccountData data = new AccountData("", "");
            string query = $"select username, password from Person.Login where username = @username and password = @password";
            using (SqlConnection conn = new SqlConnection(connection.GetConnectionString()))
            {
                conn.Open();
                using (SqlCommand queryStatus = new SqlCommand(query, conn))
                {
                    queryStatus.Parameters.AddWithValue("@username", username);
                    queryStatus.Parameters.AddWithValue("@password", password);
                    SqlDataReader reader = queryStatus.ExecuteReader();
                    while (reader.Read())
                    {
                        data = new AccountData($"{reader["username"]}", $"{reader["password"]}");
                    }
                }
            }
            return data;
        }
    }
}
