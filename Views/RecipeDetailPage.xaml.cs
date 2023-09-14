using ShoppingList.ViewModels;

namespace ShoppingList.Views;

public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPage(RecipeDetailViewModel recipeDetailViewModel)
	{
		InitializeComponent();
		BindingContext = recipeDetailViewModel;
	}
}