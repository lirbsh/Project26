using Project26.General;

namespace Project26.ModelsLogic
{
    internal class User : Models.UserModel
    {
        public override void CreateUser(string email, string password, string name)
        {
            fbd.CreateUser(email, password, name, OnCreateComplete);
        }
        public override void SignIn(string email, string password)
        {
            fbd.SignIn(email, password, OnSignInComplete);
        }
        protected override void OnCreateComplete(Task task)
        {
            UpdateStatus(task);
            if (task.IsCompletedSuccessfully)
                Save();
        }
        protected override void OnSignInComplete(Task task)
        {
            UpdateStatus(task);
        }
        protected override void UpdateStatus(Task task)
        {
            Status = task.IsCompletedSuccessfully ? Strings.Success : 
                    task.Exception != null ? fbd.GetErrorMessage(task.Exception.Message) : 
                            Strings.UnknownError;
            IsAuthenticated = task.IsCompletedSuccessfully;
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        protected override void Save()
        {
            Preferences.Set(Keys.NameKey, Name);
            Preferences.Set(Keys.EmailKey, Email);
            Preferences.Set(Keys.PasswordKey, Password);
        }
    }
}
