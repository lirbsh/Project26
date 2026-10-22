using Project26.General;

namespace Project26.ModelsLogic
{
    internal class User : Models.UserModel
    {
        public override void CreateUser(string email, string password, string name)
        {
            isBusy = true;
            fbd.CreateUser(email, password, name, OnCreateComplete);
        }
        public override void SignIn(string email, string password)
        {
            isBusy = true;
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
            isBusy = false;
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
        public static new void Test()
        {
            // This method is intentionally left empty for testing purposes.
        }
    }
}
