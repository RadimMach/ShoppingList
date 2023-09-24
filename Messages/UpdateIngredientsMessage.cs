using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ShoppingList.Messages
{
    public class UpdateIngredientsMessage : ValueChangedMessage<bool>
    {
        public UpdateIngredientsMessage(bool value) : base(value)
        {
        }
    }
}
