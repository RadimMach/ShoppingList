using ShoppingList.ViewModels;

namespace ShoppingList.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class ShoppingListPage : ContentPage
{
	public ShoppingListPage(ShoppingListViewModel shoppingListViewModel)
	{
		InitializeComponent();
		BindingContext = shoppingListViewModel;
	}
}

