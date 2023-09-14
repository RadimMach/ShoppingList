using SQLite;

namespace ShoppingList.Models
{
    [Table(nameof(Ingredient))]
    public class Ingredient : BaseEntity
    {
        [Indexed]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
