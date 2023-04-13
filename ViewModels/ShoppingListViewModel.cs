using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingList.Models;
using ShoppingList.Services;
using System.Collections.ObjectModel;

namespace ShoppingList.ViewModels
{
    public partial class ShoppingListViewModel : BaseViewModel
    {
        private readonly ShoppingDatabaseService shoppingDatabaseService;
       
        public ObservableCollection<ShopItemModel> ShopItems { get; set; }

        [ObservableProperty]
        string name;

        [ObservableProperty]
        double amount;

        [ObservableProperty]
        string unit;

        public ShoppingListViewModel(ShoppingDatabaseService databaseService)
        {
            Title = "Shopping list";
            ShopItems = new ObservableCollection<ShopItemModel>();
            shoppingDatabaseService = databaseService;

            // temp
            var items = shoppingDatabaseService.GetShopItems();
            items.ForEach(ShopItems.Add);
            ReorderShopItems();
        }

        [RelayCommand]
        void AddShopItem()
        {
            if (string.IsNullOrEmpty(Name))
                return;

            var newItem = new ShopItemModel { Name = Name };

            if (!ShopItems.Contains(newItem))
            {
                ShopItems.Add(newItem);
            }

            // add our item
            Name = string.Empty;

            OnPropertyChanged(nameof(ShopItems));
            ReorderShopItems();
        }

        [RelayCommand]
        void DeteleShopItem(ShopItemModel item)
        {
            if (ShopItems.Contains(item))
            {
                ShopItems.Remove(item);
            }
        }

        [RelayCommand]
        void CheckOffShopItem(ShopItemModel item)
        {
            item.CheckedOff = !item.CheckedOff;

            ReorderShopItems();
        }

        private void ReorderShopItems()
        {
            ShopItems = new ObservableCollection<ShopItemModel>(ShopItems.OrderBy(i => i.CheckedOff).ThenBy(i => i.Name));

            // Workaround for re-applying data template
            var tempList = ShopItems.ToList();
            ShopItems.Clear();
            ShopItems = new ObservableCollection<ShopItemModel>(tempList);
            OnPropertyChanged(nameof(ShopItems));
        }
    }
}
