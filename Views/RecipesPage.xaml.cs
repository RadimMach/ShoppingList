using ShoppingList.ViewModels;

namespace ShoppingList.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class RecipesPage : ContentPage
{
	public RecipesPage(RecipesViewModel recipesViewModel)
	{
		InitializeComponent();
		BindingContext = recipesViewModel;
	}
}