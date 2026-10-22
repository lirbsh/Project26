namespace Project26.EventsArgs
{
    internal class DeleteArgs(bool isMyDelete) : EventArgs
    {
        public bool IsMyDelete { get; set; } = isMyDelete;
    }
}