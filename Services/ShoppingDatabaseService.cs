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
        public List<ShopItem> GetShopItems()
        {
            return new List<ShopItem>()
            {
                new ShopItem() { Id = 1, Amount = 500, Name = "Milk", Unit = "ml", CheckedOff = true},
                new ShopItem() { Id = 1, Amount = 500, Name = "Beer", Unit = "ml"}
            };
        }
    }
}
