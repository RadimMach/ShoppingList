using Microsoft.Maui.Controls;
using ShoppingList.Models;
using SQLite;
using System.Linq.Expressions;

namespace ShoppingList.Services
{
    public class ShoppingDatabaseService : IShoppingDatabaseService
    {
        private SQLiteConnection conn;
        private string _dbPath;
        public string StatusMessage;
        private int result;

        public ShoppingDatabaseService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
            {
                return;
            }

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<ShopItem>();
            conn.CreateTable<Recipe>();
            conn.CreateTable<Ingredient>();
        }

        public void AddItem<T>(T item) where T : BaseEntity, new()
        {
            try
            {
                Init();

                if (item is null)
                {
                    throw new ArgumentNullException(nameof(T));
                }

                result = conn.Insert(item);
                StatusMessage = result == 0 ? "Insert failed" : "Insert successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to insert data";
            }
        }

        public IEnumerable<T> GetAllItems<T>() where T : BaseEntity, new() 
        {
            try
            {
                Init();

                return conn.Table<T>();
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return default;
        }

        public IEnumerable<T> GetItems<T>(Func<T, bool> condition) where T : BaseEntity, new()
        {
            try
            {
                Init();

                return conn.Table<T>().Where(condition);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return default;
        }

        public T GetItem<T>(int id) where T : BaseEntity, new()
        {
            try
            {
                Init();

                var item = conn.Table<T>().FirstOrDefault(x => x.Id == id);

                if (item is not null)
                {
                    return item;
                }
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return default;
        }

        public void UpdateItem<T>(T item) where T : BaseEntity, new()
        {
            try
            {
                Init();

                if (item is null)
                {
                    throw new ArgumentNullException(nameof(T));
                }

                result = conn.Update(item);
                StatusMessage = result == 0 ? "Update failed" : "Update successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to update data";
            }
        }

        public void DeleteItem<T>(T item) where T : BaseEntity, new()
        {
            try
            {
                Init();

                if (item is null)
                {
                    throw new ArgumentNullException(nameof(T));
                }

                result = conn.Delete(item);
                StatusMessage = result == 0 ? "Delete failed" : "Delete successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data";
            }
        }

        public void DeleteItems<T>(Func<T, bool> condition) where T : BaseEntity, new()
        {
            try
            {
                var itemsToDelete = GetItems(condition);
                foreach (var item in itemsToDelete)
                {
                    DeleteItem(item);
                }
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete all data.";
            }
        }

        public void AddItems<T>(IEnumerable<T> items) where T : BaseEntity, new()
        {
            try
            {
                Init();

                result = conn.InsertAll(items);
                StatusMessage = result == 0 ? "Insert failed" : "Insert successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to insert all data";
            }
        }
    }
}
