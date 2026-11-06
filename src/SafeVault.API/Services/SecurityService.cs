using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using SafeVault.API.Models;

namespace SafeVault.API.Services
{
    public class SecurityService
    {
        private readonly string _connectionString;

        public SecurityService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public User GetUserByUsername(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT Id, Username, Role FROM Users WHERE Username = @Username";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    Username = reader["Username"].ToString() ?? "",
                    Role = reader["Role"].ToString() ?? "User"
                };
            }
            return null!;
        }
    }
}
