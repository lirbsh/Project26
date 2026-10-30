namespace Project26.Models
{
    public partial class ColoredButton : Button
    {
        public Color Color { get; set; }

        public ColoredButton(int size, Color color)
        {
            BackgroundColor = color;
            Color = color;
            MinimumHeightRequest = size;
            MinimumWidthRequest = size;
        }
    }
}
