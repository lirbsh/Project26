using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Project26.Models
{
    public class AppMessage<T>(T msg) : ValueChangedMessage<T>(msg)
    {

    }
}