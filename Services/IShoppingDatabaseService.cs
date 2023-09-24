using ShoppingList.Models;

namespace ShoppingList.Services
{
    public interface IShoppingDatabaseService
    {
        void AddItem<T>(T item) where T : BaseEntity, new();
        void AddItems<T>(IEnumerable<T> items) where T : BaseEntity, new();
        void DeleteItem<T>(T item) where T : BaseEntity, new();
        void DeleteItems<T>(Func<T, bool> condition) where T : BaseEntity, new();
        IEnumerable<T> GetAllItems<T>() where T : BaseEntity, new();
        IEnumerable<T> GetItems<T>(Func<T, bool> condition) where T : BaseEntity, new();
        T GetItem<T>(int id) where T : BaseEntity, new();
        void UpdateItem<T>(T item) where T : BaseEntity, new();
    }
}