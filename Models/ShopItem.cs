using SQLite;

namespace ShoppingList.Models
{
    [Table(nameof(ShopItem))]
    public class ShopItem : BaseEntity
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public bool CheckedOff { get; set; }
    }
}
