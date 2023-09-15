using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ShoppingList.Messages;
using ShoppingList.Models;
using ShoppingList.Views;
using System.Collections.ObjectModel;

namespace ShoppingList.ViewModels
{
    public partial class RecipesViewModel : BaseViewModel, IRecipient<UpdateItemsMessage>
    {
        [ObservableProperty]
        public string name;

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
           // App.ShoppingDatabaseService.AddItem(new Recipe() { Name = "aaa" });
            await GetRecipes();
        }

        [RelayCommand]
        public async void UpdateRecipe(Recipe recipe)
        {
            await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?RecipeId={recipe.Id}");
        }

        async Task GetRecipes()
        {
            var items = App.ShoppingDatabaseService.GetItems<Recipe>().ToList();
            Recipes.Clear();
            items.ForEach(Recipes.Add);
        }

        public void Receive(UpdateItemsMessage message)
        {
            if (message.Value is true)
            {
                GetRecipes().Wait();
            }
        }
    }
}
