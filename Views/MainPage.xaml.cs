using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class MainPage : ContentPage
{
	public MainPage(ShoppingListViewModel shoppingListViewModel)
	{
		InitializeComponent();
		BindingContext = shoppingListViewModel;
	}

	
}

