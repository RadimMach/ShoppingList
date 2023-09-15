using ShoppingList.Models;

namespace ShoppingList.Services
{
    public interface IShoppingDatabaseService
    {
        void AddShopItem(ShopItem item);
        void AddItem<T>(T item) where T : BaseEntity, new();
        void DeleteShopItem(ShopItem item);
        List<ShopItem> GetShopItems();
        IEnumerable<T> GetItems<T>() where T : BaseEntity, new();
        IEnumerable<T> GetItems<T>(Func<T, bool> condition) where T : BaseEntity, new();
        T GetItem<T>(int id) where T : BaseEntity, new();
        void UpdateShopItem(ShopItem shopItem);
        void UpdateItem<T>(T item) where T : BaseEntity, new();
    }
}