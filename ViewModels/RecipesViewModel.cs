using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ShoppingList.Extensions;
using ShoppingList.Messages;
using ShoppingList.Models;
using ShoppingList.Views;
using System.Collections.ObjectModel;

namespace ShoppingList.ViewModels
{
    public partial class RecipesViewModel : BaseViewModel, IRecipient<RefreshIngredientsMessage>
    {
        [ObservableProperty]
        public string name;

        public bool IsAnyRecipe => Recipes.Count() > 0;

        public ObservableCollection<Recipe> Recipes { get; set; }

        public RecipesViewModel()
        {
            Title = "Recipes";
            Recipes = new ObservableCollection<Recipe>();

            WeakReferenceMessenger.Default.Register(this);

            GetRecipes().Wait();
        }

        [RelayCommand]
        public async void AddRecipe()
        {
            await Shell.Current.GoToAsync(nameof(RecipeDetailPage));
            await GetRecipes();
        }

        [RelayCommand]
        public async void DeleteRecipe(Recipe recipe)
        {
            if (recipe is null)
            {
                return;
            }

            App.ShoppingDatabaseService.DeleteItems<Ingredient>(i => i.RecipeId == recipe.Id);
            App.ShoppingDatabaseService.DeleteItem(recipe);

            await GetRecipes();
        }

        [RelayCommand]
        public async void IngredientsToShoppingList(Recipe recipe)
        {
            var ingredients = App.ShoppingDatabaseService.GetItems<Ingredient>(i => i.RecipeId == recipe.Id);
            var shopItems = ingredients.Select(i => i.ToShopItem());
            App.ShoppingDatabaseService.AddItems<ShopItem>(shopItems);
            WeakReferenceMessenger.Default.Send(new RefreshShopItemsMessage(true));

            await ShowAlert($"Ingredients of {recipe.Name} recipe has been added to shopping list.");
        }

        [RelayCommand]
        public async void GoToRandomRecipe()
        {
            var random = new Random();
            int randomId = random.Next(Recipes.Count);
            var recipe = Recipes[randomId];

            await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?RecipeId={recipe.Id}");
        }

        [RelayCommand]
        public async void UpdateRecipe(Recipe recipe)
        {
            await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?RecipeId={recipe.Id}");
        }

        async Task GetRecipes()
        {
            var items = App.ShoppingDatabaseService.GetAllItems<Recipe>().ToList();
            Recipes.Clear();
            items.ForEach(Recipes.Add);
            OnPropertyChanged(nameof(IsAnyRecipe));
        }

        public void Receive(RefreshIngredientsMessage message)
        {
            if (message.Value is true)
            {
                GetRecipes().Wait();
            }
        }
    }
}
