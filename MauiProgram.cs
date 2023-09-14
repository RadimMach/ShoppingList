using Microsoft.Extensions.Logging;
using ShoppingList.Effects;
using ShoppingList.Models;
using ShoppingList.Services;
using ShoppingList.ViewModels;
using ShoppingList.Views;

namespace ShoppingList;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureEffects(effects =>
			{
				effects.Add<RemoveBorderEffect, RemoveBorderPlatformEffect>();
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "shopitems.db3");

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<IShoppingDatabaseService>(s => ActivatorUtilities.CreateInstance<ShoppingDatabaseService>(s, dbPath));

		builder.Services.AddSingleton<ShoppingListViewModel>();
		builder.Services.AddSingleton<RecipesViewModel>();
		builder.Services.AddTransient<RecipeDetailViewModel>();

		builder.Services.AddSingleton<ShoppingListPage>();
		builder.Services.AddSingleton<RecipesPage>();
		builder.Services.AddTransient<RecipeDetailPage>();

		return builder.Build();
	}
}
