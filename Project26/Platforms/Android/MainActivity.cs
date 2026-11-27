using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using Java.Lang;
using Project26.General;
using Project26.Models;

namespace Project26.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        MyTimer? mTimer;
        override protected void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RegisterTimerMessages();
            RegisterAlarmMessages();
            StartDeleteFbDocsService();
        }

        private void RegisterAlarmMessages()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<AlarmSetting>>(this, (r, m) =>
            {
                OnAlarmMessageReceived(m.Value);
            });
        }

        private void StartDeleteFbDocsService()
        {
            Intent intent = new(this, typeof(DeleteFbDocsService));
            StartService(intent);
        }
        private void RegisterTimerMessages()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<TimerSettings>>(this, (r, m) =>
            {
                OnTimerMessageReceived(m.Value);
            });
            WeakReferenceMessenger.Default.Register<AppMessage<bool>>(this, (r, m) =>
            {
                OnTimerMessageReceived(m.Value);
            });
        }
        private void OnTimerMessageReceived(bool value)
        {
            if (value)
            {
                mTimer?.Cancel();
                mTimer = null;
            }
        }

        private void OnTimerMessageReceived(TimerSettings value)
        {
            mTimer = new MyTimer(value);
            mTimer.Start();
        }
        private void OnAlarmMessageReceived(AlarmSetting setting)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Intent i = new(this, typeof(AlarmReceiver));
                i.PutExtra(Keys.MessageKey, setting.MessageToSend);
                PendingIntent? pi = PendingIntent.GetBroadcast(this, Keys.AlarmRequestCode, i, PendingIntentFlags.Immutable);
                if (pi != null)
                {
                    AlarmManager? am = GetSystemService(AlarmService) as AlarmManager;
                    if (setting.CancelAlarm)
                        am?.Cancel(pi);
                    else
                        am?.Set(AlarmType.RtcWakeup, JavaSystem.CurrentTimeMillis() + setting.SecondToStart * 1000, pi);
                }
            }
        }
    }
}