using ShoppingList.ViewModels;

namespace ShoppingList.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPage(RecipeDetailViewModel recipeDetailViewModel)
	{
		InitializeComponent();
		BindingContext = recipeDetailViewModel;
	}
}