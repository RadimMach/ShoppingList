using System.Windows.Input;

namespace ShoppingList.CustomComponents;

public partial class ItemCard : ContentView
{
	public ItemCard()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TapCommandProperty =
    BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(ItemCard));

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty TapCommandParameterProperty =
    BindableProperty.Create(nameof(TapCommandParameter), typeof(object), typeof(ItemCard));

    public object TapCommandParameter
    {
        get => GetValue(TapCommandParameterProperty);
        set => SetValue(TapCommandParameterProperty, value);
    }

    public static readonly BindableProperty ItemNameProperty =
    BindableProperty.Create(nameof(ItemName), typeof(string), typeof(ItemCard));

    public string ItemName
    {
        get => (string)GetValue(ItemNameProperty);
        set => SetValue(ItemNameProperty, value);
    }

    public static readonly BindableProperty ItemAmountProperty =
    BindableProperty.Create(nameof(ItemAmount), typeof(string), typeof(ItemCard));

    public string ItemAmount
    {
        get => (string)GetValue(ItemAmountProperty);
        set => SetValue(ItemAmountProperty, value);
    }

    public static readonly BindableProperty ItemUnitProperty =
    BindableProperty.Create(nameof(ItemUnit), typeof(string), typeof(ItemCard));

    public string ItemUnit
    {
        get => (string)GetValue(ItemUnitProperty);
        set => SetValue(ItemUnitProperty, value);
    }

    public static readonly BindableProperty ItemBackgroundColorProperty =
    BindableProperty.Create(nameof(ItemBackgroundColor), typeof(Color), typeof(ItemCard));

    public Color ItemBackgroundColor
    {
        get => (Color)GetValue(ItemBackgroundColorProperty);
        set => SetValue(ItemBackgroundColorProperty, value);
    }
}