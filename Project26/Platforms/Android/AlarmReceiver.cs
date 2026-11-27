using Android.Content;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Project26.General;

namespace Project26.Platforms.Android
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context? context, Intent? intent)
        {
            if (intent != null )
            {
                string? msg = intent.GetStringExtra(Keys.MessageKey);
                if (msg != null) 
                    Toast.Make(msg, ToastDuration.Long,14).Show();
            }
        }
    }
}
