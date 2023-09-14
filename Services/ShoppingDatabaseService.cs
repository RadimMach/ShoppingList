﻿using Microsoft.Maui.Controls;
using ShoppingList.Models;
using SQLite;

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

        public void AddShopItem(ShopItem item)
        {
            try
            {
                Init();

                if (item is null)
                {
                    throw new ArgumentNullException("item");
                }

                result = conn.Insert(item);
                StatusMessage = result == 0 ? "Insert failed" : "Insert successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to insert data";
            }
        }

        public void DeleteShopItem(ShopItem item)
        {
            try
            {
                Init();

                result = conn.Delete(item);
                StatusMessage = result == 0 ? "Delete failed" : "Delete successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data";
            }
        }

        public List<ShopItem> GetShopItems()
        {
            try
            {
                Init();

                return conn.Table<ShopItem>().ToList();
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return new List<ShopItem>();
        }

        public void UpdateShopItem(ShopItem shopItem)
        {
            try
            {
                Init();

                result = conn.Update(shopItem);
                StatusMessage = result == 0 ? "Update failed" : "Update successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to update data";
            }
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

        public IEnumerable<T> GetItems<T>() where T : BaseEntity, new() 
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
    }
}
