using MyProject.Models;

namespace MyProject.Services
{
    public class UserService
    {
        private User currentUser = null;

        public event Action OnUserChanged;

        public void SetUser(User user)
        {
            currentUser = user;
            OnUserChanged?.Invoke();
        }

        public User GetUser()
        {
            return currentUser;
        }

        public string GetFullName() => currentUser?.GetFullName() ?? "";
        public string GetRole() => currentUser?.GetRole() ?? "";
        public bool IsLoggedIn() => currentUser != null;
        public void Logout()
        {
            currentUser = null;
            OnUserChanged?.Invoke();
        }
    }
}

