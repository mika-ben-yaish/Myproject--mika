using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using MyProject.Models;


namespace MyProject.Data
{

    public class MainDBService
    {
        private readonly string _connectionString = "Data Source=New Table.db;Version=3;";

        // קריאה של כל המשתמשים
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("SELECT Id, FullName, Email, Password, Role FROM NewTable", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                User user = new User();
                user.SetId(reader.GetInt32(0));
                user.SetFullName(Convert.ToString(reader[1]));
                user.SetEmail(Convert.ToString(reader[2]));
                user.SetPassword(Convert.ToString(reader[3]));
                user.SetRole(reader.IsDBNull(4) ? null : Convert.ToString(reader[4]));
                users.Add(user);
            }

            reader.Close();
            connection.Close();

            return users;
        }

        // הוספת משתמש חדש
        public void AddUser(string fullName, string email, string password, string role)
        {
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(
                "INSERT INTO NewTable (FullName, Email, Password, Role) VALUES (@FullName, @Email, @Password, @Role)",
                connection);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Role", role);

            command.ExecuteNonQuery();
            connection.Close();
        }
        public bool VerifyUser(string username, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM tabletwo WHERE username = @username AND password = @password";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int userCount = (int)command.ExecuteScalar();
                        return userCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while verifying user: " + ex.Message);
            }
        }



        // מחיקת משתמש לפי מזהה
        public void DeleteUser(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand("DELETE FROM NewTable WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();

            connection.Close();
        }


        public void UpdateUserRole(int id, string role)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(
                    "UPDATE NewTable SET Role = @Role WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        // עדכון פרטי משתמש לפי מזהה
        public void UpdateUser(int id, string fullName, string email, string password, string role)
        {
            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            SQLiteCommand command = new SQLiteCommand(
                "UPDATE NewTable SET FullName = @FullName, Email = @Email, Password = @Password, Role = @Role WHERE Id = @Id",
                connection);
            command.Parameters.AddWithValue("@FullName", fullName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@Role", role);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

