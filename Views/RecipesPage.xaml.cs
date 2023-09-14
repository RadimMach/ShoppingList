using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class RecipesPage : ContentPage
{
	public RecipesPage(RecipesViewModel recipesViewModel)
	{
		InitializeComponent();
		BindingContext = recipesViewModel;
	}
}