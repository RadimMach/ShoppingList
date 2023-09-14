using SQLite;

namespace ShoppingList.Models
{
    [Table(nameof(Recipe))]
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
