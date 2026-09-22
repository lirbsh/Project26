using Project26.General;
using Project26.ModelsLogic;

namespace Project26.Models
{
    internal abstract class UserModel
    {
        protected FbData fbd = new();
        public EventHandler? StatusChanged;
        public string Email { get; set; } = Preferences.Get(Keys.EmailKey, string.Empty);   
        public string Password { get; set; } = Preferences.Get(Keys.PasswordKey, string.Empty);
        public string Name { get; set; } = Preferences.Get(Keys.NameKey, string.Empty);
        public string Status { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
        public abstract void CreateUser(string email, string password, string name);
        public abstract void SignIn(string email, string password);
        protected abstract void OnCreateComplete(Task task);
        protected abstract void OnSignInComplete(Task task);
        protected abstract void UpdateStatus(Task task);
        protected abstract void Save();
    }
}
