using Android.OS;
using CommunityToolkit.Mvvm.Messaging;
using Project26.Models;
using Project26.General;
using static Project26.Models.AnimatiomsModel;

namespace Project26.Platforms.Android
{
    public class MyTimer(TimerSettings timerSettings) : CountDownTimer(timerSettings.TotalTimeInMilliseconds, timerSettings.IntervalInMilliseconds)
    {
        private readonly AnimationTypes animationType = timerSettings.AnimationType;
        public override void OnFinish()
        {
            if (animationType == AnimationTypes.Opacity)
                WeakReferenceMessenger.Default.Send(new AppMessage<long>(Keys.FinishedSignal));
            else
                WeakReferenceMessenger.Default.Send(new AppMessage<int>((int)Keys.FinishedSignal));
        }
        public override void OnTick(long millisUntilFinished)
        {

            if (animationType == AnimationTypes.Opacity)
                WeakReferenceMessenger.Default.Send(new AppMessage<long>(millisUntilFinished));
            else
                WeakReferenceMessenger.Default.Send(new AppMessage<int>((int)millisUntilFinished));
        }
    }
}