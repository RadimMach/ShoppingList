using CommunityToolkit.Mvvm.ComponentModel;
using ShoppingList.Models;
using System.Collections.ObjectModel;

namespace ShoppingList.ViewModels
{
    [QueryProperty(nameof(RecipeId), "RecipeId")]
    public partial class RecipeDetailViewModel : BaseViewModel
    {
        private int recipeId;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        //Recipe recipe;

        public int RecipeId
        {
            set
            {
                recipeId = value;
                if (recipeId > 0)
                {
                    Ingredients = new ObservableCollection<Ingredient>(App.ShoppingDatabaseService.GetItems<Ingredient>(x => x.RecipeId == recipeId));
                    var recipe = App.ShoppingDatabaseService.GetItem<Recipe>(recipeId);
                    Name = recipe.Name;
                    Description = recipe.Description;
                }
            }
            get => recipeId;
        }

        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public RecipeDetailViewModel()
        {
                
        }
    }
}
