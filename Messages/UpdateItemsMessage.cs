using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ShoppingList.Messages
{
    public class UpdateItemsMessage : ValueChangedMessage<bool>
    {
        public UpdateItemsMessage(bool value) : base(value)
        {
        }
    }
}
