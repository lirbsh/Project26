namespace Project26.Models
{
    internal abstract class AppPermissionsModel
    {
        public bool IsNotificationPermissionGranted { get; protected set; }
        public abstract void AskPermissions();
    }
}
