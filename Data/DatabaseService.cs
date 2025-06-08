using System.Data.SQLite;


namespace MyProject.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source=New Table.db;Version=3;";

        public DatabaseService()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT);", connection);
            command.ExecuteNonQuery();
        }

        public List<string> GetUsers()
        {
            var users = new List<string>();
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var command = new SQLiteCommand("SELECT Name FROM Users", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(reader.GetString(0));
            }
            return users;
        }

        public void AddUser(string name)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var command = new SQLiteCommand("INSERT INTO Users (Name) VALUES (@Name)", connection);
            command.Parameters.AddWithValue("@Name", name);
            command.ExecuteNonQuery();
        }


       



        private List<string> questions = new List<string>();

        public List<string> GetQuestions()
        {
            return questions;
        }

        public void AddQuestion(string question)
        {
            if (!string.IsNullOrWhiteSpace(question))
            {
                questions.Add(question);
            }
        }

    }
}


