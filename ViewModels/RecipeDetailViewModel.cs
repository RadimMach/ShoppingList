﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        string recipeName;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        Ingredient selectedIngredient;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAnyEntryFilled))]
        [NotifyPropertyChangedFor(nameof(IsAddingItem))]
        [NotifyPropertyChangedFor(nameof(IsUpdatingItem))]
        string name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAnyEntryFilled))]
        [NotifyPropertyChangedFor(nameof(IsAddingItem))]
        [NotifyPropertyChangedFor(nameof(IsUpdatingItem))]
        double amount;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAnyEntryFilled), nameof(IsAddingItem), nameof(IsUpdatingItem))]
        string unit;

        public int RecipeId
        {
            set
            {
                recipeId = value;
                if (recipeId > 0)
                {
                    Ingredients = new ObservableCollection<Ingredient>(App.ShoppingDatabaseService.GetItems<Ingredient>(x => x.RecipeId == recipeId));
                    recipe = App.ShoppingDatabaseService.GetItem<Recipe>(recipeId);
                    RecipeName = recipe.Name;
                    Description = recipe.Description;
                }
                GetIngredients().Wait();
                OnPropertyChanged(nameof(IsNewRecipe));
                OnPropertyChanged(nameof(Ingredients));
            }
            get => recipeId;
        }

        public ObservableCollection<Ingredient> Ingredients { get; set; }

        private Recipe recipe;

        public RecipeDetailViewModel()
        {
            Title = "Recipe detail";
            Ingredients = new ObservableCollection<Ingredient>();
        }

        public bool IsAnyEntryFilled => !string.IsNullOrEmpty(Name) 
            || !string.IsNullOrEmpty(Unit) || Amount > 0;

        public bool IsAddingItem => IsAnyEntryFilled && SelectedIngredient is null;

        public bool IsUpdatingItem => IsAnyEntryFilled && SelectedIngredient != null;

        public bool IsNewRecipe => recipe is null;

        async Task GetIngredients()
        {
            var items = App.ShoppingDatabaseService.GetItems<Ingredient>(x => x.RecipeId == recipeId).ToList();
            Ingredients.Clear();
            items.ForEach(Ingredients.Add);
            SelectedIngredient = null;
        }

        [RelayCommand]
        void Tap(Ingredient ingredient)
        {
            Name = ingredient.Name;
            Amount = ingredient.Amount;
            Unit = ingredient.Unit;
            SelectedIngredient = ingredient;
        }

        [RelayCommand]
        void ExitEditMode()
        {
            ClearEntries();
            SelectedIngredient = null;
        }

        [RelayCommand]
        public async Task AddIngredient()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }

            var item = new Ingredient { Name = Name, Amount = Amount, Unit = Unit, RecipeId = recipe.Id };

            if (!Ingredients.Contains(item))
            {
                Ingredients.Add(item);
            }

            App.ShoppingDatabaseService.AddItem<Ingredient>(item);

            await GetIngredients();
            ClearEntries();
        }

        [RelayCommand]
        public async Task UpdateIngredient()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }

            SelectedIngredient.Amount = Amount;
            SelectedIngredient.Name = Name;
            SelectedIngredient.Unit = Unit;

            App.ShoppingDatabaseService.UpdateItem<Ingredient>(SelectedIngredient);

            await GetIngredients();
            ClearEntries();
        }

        [RelayCommand]
        public async void AddRecipe()
        {
            if (string.IsNullOrEmpty(RecipeName))
            {
                return;
            }

            recipe = new Recipe() { Name = RecipeName, Description = Description };

            App.ShoppingDatabaseService.AddItem(recipe);

            WeakReferenceMessenger.Default.Send(new UpdateItemsMessage(true));
            OnPropertyChanged(nameof(IsNewRecipe));
        }

        [RelayCommand]
        public async Task UpdateRecipe()
        {
            if (string.IsNullOrEmpty(RecipeName))
            {
                return;
            }

            recipe.Name = RecipeName;
            recipe.Description = Description;

            App.ShoppingDatabaseService.UpdateItem(recipe);

            WeakReferenceMessenger.Default.Send(new UpdateItemsMessage(true));
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private void ClearEntries()
        {
            Name = string.Empty;
            Amount = default;
            Unit = string.Empty;
            SelectedIngredient = null;
        }
    }
}
