using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class ShopItemModel : BaseEntity
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public bool CheckedOff { get; set; }
    }
}
