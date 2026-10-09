namespace Project26.Models
{
    internal class DeleteArgs(bool isMyDelete) : EventArgs
    {
        public bool IsMyDelete { get; set; } = isMyDelete;
    }
}
