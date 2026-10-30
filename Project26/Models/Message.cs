namespace Project26.Models
{
    internal class Message
    {
        public string Text { get; set; } = string.Empty;
        public bool IsBold { get; set; }
        public Color Color { get; set; } = Colors.Black;
        public bool IsHostMessage { get; set; } = true;
    }
}