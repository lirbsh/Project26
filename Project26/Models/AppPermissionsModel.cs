namespace Project26.Models
{
    internal abstract class AppPermissionsModel
    {
        public abstract bool CheckNotificationPermission();
        public abstract void AskNotificationPermission();
    }
}
