using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ShoppingList.Messages;
using ShoppingList.Models;
using System.Collections.ObjectModel;

namespace ShoppingList.ViewModels
{
    public partial class ShoppingListViewModel : BaseViewModel, IRecipient<RefreshShopItemsMessage>
    {
        public ObservableCollection<ShopItem> ShopItems { get; set; }

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

        [ObservableProperty]
        ShopItem selectedItem;


        public bool IsAnyEntryFilled => !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Unit) || Amount > 0;

        public bool IsAddingItem => IsAnyEntryFilled && SelectedItem is null;

        public bool IsUpdatingItem => IsAnyEntryFilled && SelectedItem != null;

        public ShoppingListViewModel()
        {
            Title = "Shopping list";
            ShopItems = new ObservableCollection<ShopItem>();

            WeakReferenceMessenger.Default.Register(this);
            GetShopItems().Wait();
        }

        async Task GetShopItems()
        {
            var items = App.ShoppingDatabaseService.GetAllItems<ShopItem>().ToList();
            ShopItems.Clear();
            items.ForEach(ShopItems.Add);
            ReorderShopItems();
            SelectedItem = null;
        }

        [RelayCommand]
        async Task AddShopItem()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return;
            }

            var shopItem = new ShopItem { Name = Name, Amount = Amount, Unit = Unit };

            if (!ShopItems.Contains(shopItem))
            {
                ShopItems.Add(shopItem);
            }

            App.ShoppingDatabaseService.AddItem(shopItem);
            await GetShopItems();

            ClearEntries();
        }

        private void ClearEntries()
        {
            Name = string.Empty;
            Amount = default;
            Unit = string.Empty;
        }

        [RelayCommand]
        async Task DeteleShopItem(ShopItem shopItem)
        {
            if (shopItem is null)
            {
                return;
            }

            App.ShoppingDatabaseService.DeleteItem(shopItem);
            await GetShopItems();
        }

        [RelayCommand]
        async Task UpdateShopItem()
        {
            if (SelectedItem is null)
            {
                return;
            }

            var item = new ShopItem
            {
                Id = SelectedItem.Id,
                Name = Name,
                Amount = Amount,
                Unit = Unit,
            };

            App.ShoppingDatabaseService.UpdateItem(item);
            await GetShopItems();

            ClearEntries();
        }

        [RelayCommand]
        async Task CheckOffShopItem(ShopItem shopItem)
        {
            shopItem.CheckedOff = !shopItem.CheckedOff;

            App.ShoppingDatabaseService.UpdateItem(shopItem);
            await GetShopItems();
        }

        [RelayCommand]
        void Tap(ShopItem shopItem)
        {
            Name = shopItem.Name;
            Amount = shopItem.Amount;
            Unit = shopItem.Unit;
        }

        [RelayCommand]
        async void DeletePurchasedItems()
        {
            App.ShoppingDatabaseService.DeleteItems<ShopItem>(i => i.CheckedOff);
            await GetShopItems();
        }

        [RelayCommand]
        async void DeleteAllItems()
        {
            App.ShoppingDatabaseService.DeleteItems<ShopItem>(i => true);
            await GetShopItems();
        }

        [RelayCommand]
        void ExitEditMode()
        {
            ClearEntries();
            SelectedItem = null;
        }

        private void ReorderShopItems()
        {
            ShopItems = new ObservableCollection<ShopItem>(ShopItems.OrderBy(i => i.CheckedOff).ThenBy(i => i.Name));
            OnPropertyChanged(nameof(ShopItems));
        }

        public async void Receive(RefreshShopItemsMessage message)
        {
            if (message.Value is true)
            {
                await GetShopItems();
            }
        }
    }
}
