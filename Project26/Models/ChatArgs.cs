using Project26.ModelsLogic;

namespace Project26.Models
{
    internal class ChatArgs(Chat chat,bool created) : EventArgs
    {
        public Chat Chat { get; set; } = chat;
        public bool Created { get; set; } = created;
    }
}
