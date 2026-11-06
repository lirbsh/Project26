namespace Project26.Models
{
    internal class CustomColor
    {
        public event EventHandler? ColorChanged;
        public void OnColorChanged() => ColorChanged?.Invoke(this, EventArgs.Empty);
        public double Red { get; set; } = 128;
        public double Green { get; set; } = 128;
        public double Blue { get; set; } = 128;
        public double Alpha { get; set; } = 1.0;
        public Color SelectedColor => Color.FromRgba((float)(Red / 255.0), (float)(Green / 255.0), (float)(Blue / 255.0), Alpha);
        public string HexValue => SelectedColor.ToHex();
    }
}
