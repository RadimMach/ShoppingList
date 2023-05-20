using ShoppingList.Services;

namespace ShoppingList;

public partial class App : Application
{
	public static IShoppingDatabaseService ShoppingDatabaseService { get; private set; }
	public App(IShoppingDatabaseService shoppingDatabaseService)
	{
		InitializeComponent();

        MainPage = new AppShell();
		ShoppingDatabaseService = shoppingDatabaseService;
	}
}
