using ShoppingList.Views;

namespace ShoppingList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RecipeDetailPage), typeof(RecipeDetailPage)); ;
	}
}
