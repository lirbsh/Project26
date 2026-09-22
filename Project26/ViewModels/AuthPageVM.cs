using System.Windows.Input;

namespace Project26.ViewModels
{
    internal partial class AuthPageVM : Models.ObservableObject
    {
        private readonly ModelsLogic.User user = new();
        public string Email { get =>user.Email; set=>user.Email = value; }
        public string Password { get => user.Password; set => user.Password = value; }
        public string Name { get => user.Name; set => user.Name = value; }
        public string Status { get => user.Status; set => user.Status = value; }
        public ICommand CreateUserCommand => new Command(CreateUser);

        private void CreateUser()
        {
            user.CreateUser(Email, Password, Name);
        }

        public ICommand SignInCommand => new Command(SignIn);

        private void SignIn()
        {
            user.SignIn(Email, Password);
        }
        public AuthPageVM()
        {
            user.StatusChanged += OnStatusChanged;
        }

        private void OnStatusChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Status));
            if (user.IsAuthenticated)
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current?.Windows[0].Page  = new AppShell();
                });
        }
    }
}
