using ShoppingList.Models;

namespace ShoppingList.Extensions
{
    public static class IngredientExtension
    {
        public static ShopItem ToShopItem(this Ingredient ingredient)
        {
            return new ShopItem() { Name = ingredient.Name, Amount = ingredient.Amount, Unit = ingredient.Unit };
        }
    }
}
