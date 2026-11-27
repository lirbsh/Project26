using Java.Lang;
using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class AppPermissions : AppPermissionsModel
    {
        public static AppPermissions Instance =>
            IPlatformApplication.Current?.Services.GetService<AppPermissions>() ?? new AppPermissions();

        public override bool CheckNotificationPermission()
        {
            PermissionStatus status =  Permissions.CheckStatusAsync<Permissions.PostNotifications>().Result;
            return status == PermissionStatus.Granted;
        }

        public override void AskNotificationPermission()
        {
            bool isGranted = CheckNotificationPermission();
            if(!isGranted)
                 Permissions.RequestAsync<Permissions.PostNotifications>();
        }
    }
}
