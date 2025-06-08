namespace MyProject.Models

{
    public class User
    {
        private int id;
        private string fullName;
        private string email;
        private string password;
        private string role;

        public User() 
        
        { }

        public User(int id, string fullName, string email, string password, string role)
        {
            this.id = id;
            this.fullName = fullName;
            this.email = email;
            this.password = password;
            this.role = role;
        }

        public int GetId()
        {
            return id;
        }

        public void SetId(int value)
        {
            id = value;
        }

        public string GetFullName()
        {
            return fullName;
        }

        public void SetFullName(string value)
        {
            fullName = value;
        }

        public string GetEmail()
        {
            return email;
        }

        public void SetEmail(string value)
        {
            email = value;
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetPassword(string value)
        {
            password = value;
        }

        public string GetRole()
        {
            return role;
        }

        public void SetRole(string value)
        {
            role = value;
        }
    }
}