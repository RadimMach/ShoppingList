namespace ShoppingList.CustomComponents;

public partial class CustomSwipeItem : ContentView
{
	public static readonly BindableProperty SwipeBackgroundProperty =
		BindableProperty.Create(nameof(SwipeBackground), typeof(Brush), typeof(CustomSwipeItem));

	public Brush SwipeBackground
	{
		get => (Brush)GetValue(SwipeBackgroundProperty);
		set => SetValue(SwipeBackgroundProperty, value);
	}

	public static readonly BindableProperty SwipeTextProperty =
		BindableProperty.Create(nameof(SwipeText), typeof(string), typeof(CustomSwipeItem));

	public string SwipeText
	{
		get => (string)GetValue(SwipeTextProperty);
		set => SetValue(SwipeTextProperty, value);
	}

	public static readonly BindableProperty SwipeHorizontalOptionsProperty =
		BindableProperty.Create(nameof(SwipeHorizontalOptions), typeof(LayoutOptions), typeof(CustomSwipeItem));

	public LayoutOptions SwipeHorizontalOptions
    {
		get => (LayoutOptions)GetValue(SwipeHorizontalOptionsProperty);
		set => SetValue(SwipeHorizontalOptionsProperty, value);
	}

	public CustomSwipeItem()
	{
		InitializeComponent();
	}
}