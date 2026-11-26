using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class AppPermissions : AppPermissionsModel
    {
        public static AppPermissions Instance =>
            IPlatformApplication.Current?.Services.GetService<AppPermissions>() ?? new AppPermissions();
        public async override void AskPermissions()
        {
            PermissionStatus ps = await Permissions.RequestAsync<Permissions.PostNotifications>();
            IsNotificationPermissionGranted = ps == PermissionStatus.Granted;
        }
    }
}
