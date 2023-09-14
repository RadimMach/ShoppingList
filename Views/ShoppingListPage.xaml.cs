using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class ShoppingListPage : ContentPage
{
	public ShoppingListPage(ShoppingListViewModel shoppingListViewModel)
	{
		InitializeComponent();
		BindingContext = shoppingListViewModel;
	}
}

