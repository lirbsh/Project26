namespace Project26.Models
{
    internal class ChatChangedArgs(string message): EventArgs
    {
        public string Message { get; set; } = message;
    }
}
