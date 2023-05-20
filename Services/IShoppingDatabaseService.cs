using ShoppingList.Models;

namespace ShoppingList.Services
{
    public interface IShoppingDatabaseService
    {
        void AddShopItem(ShopItem item);
        void DeleteShopItem(ShopItem item);
        List<ShopItem> GetShopItems();
        void UpdateShopItem(ShopItem shopItem);
    }
}