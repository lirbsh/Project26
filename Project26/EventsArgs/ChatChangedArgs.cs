namespace Project26.EventsArgs
{
    internal class ChatChangedArgs(string status): EventArgs
    {
        public string Status { get; set; } = status;
    }
}