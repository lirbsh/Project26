using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project26.General;
using Project26.Models;
using Project26.ModelsLogic;

namespace Project26.ViewModels
{
    internal partial class AuthPageVM : ObservableObject
    {
        private AlarmSetting alarmSetting;
        private readonly AppPermissions appPermissions = AppPermissions.Instance;
        private readonly Animatioms animatioms = new() { IsLooping = true };
        private readonly User user = User.Instance;
        public string Email { get =>user.Email; set=>user.Email = value; }
        public string Password { get => user.Password; set => user.Password = value; }
        public string Name { get => user.Name; set => user.Name = value; }
        public string Status { get => user.Status; set => user.Status = value; }
        public double AnimationOpacity => animatioms.Opacity;
        public string UserStateAction => animatioms.Text;
        public AuthPageVM()
        {
            user.StatusChanged += OnStatusChanged;
            animatioms.OpacityChanged += OnOpacityChanged;
            animatioms.TextChanged += OnTextChanged;
        }
        private void OnTextChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(UserStateAction));
        }
        private void OnOpacityChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(AnimationOpacity));
        }

        [RelayCommand(CanExecute = nameof(CanCreateUser))]
        private void CreateUser()
        {
            user.CreateUser(Email, Password, Name);
            CreateUserCommand.NotifyCanExecuteChanged();
        }
        [RelayCommand]
        private static void Quit()
        {
            Application.Current?.Quit();
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
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                OnPropertyChanged(nameof(Status));
                if (user.IsAuthenticated)
                {
                    alarmSetting.CancelAlarm = true;
                    WeakReferenceMessenger.Default.Send(new AppMessage<AlarmSetting>(alarmSetting));
                    Application.Current?.Windows[0].Page = new AppShell();
                }
                else
                {
                    SignInCommand.NotifyCanExecuteChanged();
                    CreateUserCommand.NotifyCanExecuteChanged();
                    SetAlarm();
                }
            });
        }
        internal void StartAnimations()
        {
            animatioms.StartOpacityAnimation();
            animatioms.StartTextAnimation(user.IsRegistered ? Strings.Login : Strings.Register);
        }
        internal void StopAnimations()
        {
            animatioms.StopAnimations();
        }

        internal static bool OnBackButtonPressed()
        {
            Toast.Make(Strings.PreventBackMsg, ToastDuration.Long, 14).Show();
            return true; // Prevent back navigation
        }

        internal void AskPermissions()
        {
            appPermissions.AskNotificationPermission();
        }

        internal void SetAlarm()
        {
            string msg = user.IsRegistered ? Strings.PleaseLogin : Strings.PleaseRegister;
            alarmSetting = new AlarmSetting(false, 5, msg);
            WeakReferenceMessenger.Default.Send(new AppMessage<AlarmSetting>(alarmSetting));
        }
    }
}