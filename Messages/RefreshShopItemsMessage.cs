using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ShoppingList.Messages
{
    public class RefreshShopItemsMessage : ValueChangedMessage<bool>
    {
        public RefreshShopItemsMessage(bool value) : base(value)
        {
        }
    }
}
