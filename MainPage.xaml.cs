using ShoppingList.ViewModels;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
	public MainPage(ShoppingListViewModel shoppingListViewModel)
	{
		InitializeComponent();
		BindingContext = shoppingListViewModel;
	}

	
}

