using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ShoppingList.Messages
{
    public class RefreshIngredientsMessage : ValueChangedMessage<bool>
    {
        public RefreshIngredientsMessage(bool value) : base(value)
        {
        }
    }
}
