using static Project26.Models.AnimatiomsModel;

namespace Project26.Models
{
    public class TimerSettings(long totalTimeInMilliseconds, long intervalInMilliseconds)
    {
        public long TotalTimeInMilliseconds { get; set; } = totalTimeInMilliseconds;
        public long IntervalInMilliseconds { get; set; } = intervalInMilliseconds;
        public AnimationTypes AnimationType { get; set; } = AnimationTypes.Opacity;
    }
}
