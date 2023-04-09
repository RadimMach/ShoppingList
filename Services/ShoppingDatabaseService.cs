using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
    public class ShoppingDatabaseService
    {
        public List<ShopItemModel> GetShopItems()
        {
            return new List<ShopItemModel>()
            {
                new ShopItemModel() { Id = 1, Amount = 500, Name = "Milk", Unit = "ml"},
                new ShopItemModel() { Id = 1, Amount = 500, Name = "Beer", Unit = "ml"}
            };
        }
    }
}
