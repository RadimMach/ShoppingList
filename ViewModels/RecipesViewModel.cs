using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Models;
using ShoppingList.Views;
using System.Collections.ObjectModel;

namespace ShoppingList.ViewModels
{
    public partial class RecipesViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string name;

        public ObservableCollection<Recipe> Recipes { get; set; }


        public RecipesViewModel()
        {
            Title = "Recipes";
            Recipes = new ObservableCollection<Recipe>();

            GetRecipes().Wait();
        }


        [RelayCommand]
        public async void AddRecipe()
        {
            await Shell.Current.GoToAsync(nameof(RecipeDetailPage));
           // App.ShoppingDatabaseService.AddItem(new Recipe() { Name = "aaa" });
            await GetRecipes();
        }

        [RelayCommand]
        public async void UpdateRecipe(Recipe recipe)
        {
            await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?RecipeId={recipe.Id}");
            await GetRecipes();
        }

        async Task GetRecipes()
        {
            var items = App.ShoppingDatabaseService.GetItems<Recipe>().ToList();
            Recipes.Clear();
            items.ForEach(Recipes.Add);
        }
    }
}
