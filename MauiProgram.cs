using Microsoft.Extensions.Logging;
using ShoppingList.Effects;
using ShoppingList.Models;
using ShoppingList.Services;
using ShoppingList.ViewModels;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<ShoppingDatabaseService>();

		builder.Services.AddSingleton<ShoppingListViewModel>();
		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
