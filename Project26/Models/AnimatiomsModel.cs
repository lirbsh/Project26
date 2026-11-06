using Project26.General;
namespace Project26.Models
{
    public abstract class AnimatiomsModel
    {
        protected bool incrementOpacity = true;
        protected string textToAnimate = string.Empty;
        protected TimerSettings timerSettings = new(Keys.TotalAnimationTime, Keys.AnimationTimeInterval);
        public event EventHandler? OpacityChanged;
        protected void OnOpacityChanged() => OpacityChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler? TextChanged;
        protected void OnTextChanged() => TextChanged?.Invoke(this, EventArgs.Empty);
        public enum AnimationTypes { Opacity, Text }
        public double Opacity { get; protected set; }
        public string Text { get; protected set; } = string.Empty;
        public bool IsLooping { get; set; }
        public abstract void StartOpacityAnimation();
        public abstract void StartTextAnimation(string text);
    }
}
