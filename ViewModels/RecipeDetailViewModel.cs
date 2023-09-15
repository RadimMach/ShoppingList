using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ShoppingList.Messages;
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

        public int RecipeId
        {
            set
            {
                recipeId = value;
                if (recipeId > 0)
                {
                    Ingredients = new ObservableCollection<Ingredient>(App.ShoppingDatabaseService.GetItems<Ingredient>(x => x.RecipeId == recipeId));
                    recipe = App.ShoppingDatabaseService.GetItem<Recipe>(recipeId);
                    Name = recipe.Name;
                    Description = recipe.Description;
                }
            }
            get => recipeId;
        }

        public ObservableCollection<Ingredient> Ingredients { get; set; }

        private Recipe recipe;

        public RecipeDetailViewModel()
        {
                
        }

        [RelayCommand]
        public async Task Update()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }

            recipe.Name = Name;
            recipe.Description = Description;

            App.ShoppingDatabaseService.UpdateItem(recipe);

            // add ingredients algorithm

            WeakReferenceMessenger.Default.Send(new UpdateItemsMessage(true));
            await Shell.Current.GoToAsync("..");
        }
    }
}
