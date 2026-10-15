using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Project26.ViewModels
{
    internal partial class AuthPageVM : ObservableObject
    {
       
        private readonly ModelsLogic.User user = new();
        public string Email { get =>user.Email; set=>user.Email = value; }
        public string Password { get => user.Password; set => user.Password = value; }
        public string Name { get => user.Name; set => user.Name = value; }
        public string Status { get => user.Status; set => user.Status = value; }
   
        public AuthPageVM()
        {
            user.StatusChanged += OnStatusChanged;
        }
        [RelayCommand(CanExecute = nameof(CanCreateUser))]
        private void CreateUser()
        {
            user.CreateUser(Email, Password, Name);
            CreateUserCommand.NotifyCanExecuteChanged();
        }

        private bool CanCreateUser()
        {
            return user.CanCreate;
        }

        [RelayCommand(CanExecute = nameof(CanSignIn))]
        private void SignIn()
        {
            user.SignIn(Email, Password);
            SignInCommand.NotifyCanExecuteChanged();
        }

        private bool CanSignIn()
        {
            return user.CanSignIn;
        }

        private void OnStatusChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Status));
            if (user.IsAuthenticated)
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current?.Windows[0].Page  = new AppShell();
                });
            else
            {
                SignInCommand.NotifyCanExecuteChanged();
                CreateUserCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
