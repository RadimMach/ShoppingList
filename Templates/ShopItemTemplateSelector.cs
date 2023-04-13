using ShoppingList.Models;

namespace ShoppingList.Templates
{
    public class ShopItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultShopItemTemplate { get; set; }
        public DataTemplate CheckedOffShopItemTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ShopItemModel)item).CheckedOff ? CheckedOffShopItemTemplate : DefaultShopItemTemplate ;
        }
    }
}
