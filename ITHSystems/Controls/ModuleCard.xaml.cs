using System.Windows.Input;

namespace ITHSystems.Controls;

public partial class ModuleCard : ContentView
{
	public ModuleCard()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty BadgesProperty =
       BindableProperty.Create(
           nameof(Badges),
           typeof(int),
           typeof(ModuleCard),
           default(int));

    public int Badges
    {
        get => (int)GetValue(BadgesProperty);
        set => SetValue(BadgesProperty, value);
    }

    public static readonly BindableProperty ShowBadgesProperty =
       BindableProperty.Create(
           nameof(ShowBadges),
           typeof(bool),
           typeof(ModuleCard),
           default(bool));

    public bool ShowBadges
    {
        get => (bool)GetValue(ShowBadgesProperty);
        set => SetValue(ShowBadgesProperty, value);
    }

    public static readonly BindableProperty BGColorProperty =
       BindableProperty.Create(
           nameof(BGColor),
           typeof(Brush),
           typeof(ModuleCard),
           default(Brush));

    public Brush BGColor
    {
        get => (Brush)GetValue(BGColorProperty);
        set => SetValue(BGColorProperty, value);
    }

    public static readonly BindableProperty ModuleNameProperty =
       BindableProperty.Create(
           nameof(ModuleName),
           typeof(string),
           typeof(ModuleCard),
           default(string));

    public string ModuleName
    {
        get => (string)GetValue(ModuleNameProperty);
        set => SetValue(ModuleNameProperty, value);
    }

    public static readonly BindableProperty ModuleFontIconProperty =
       BindableProperty.Create(
           nameof(ModuleFontIcon),
           typeof(string),
           typeof(ModuleCard),
           default(string));

    public string ModuleFontIcon
    {
        get => (string)GetValue(ModuleFontIconProperty);
        set => SetValue(ModuleFontIconProperty, value);
    }

    public static readonly BindableProperty ModuleFontFamilyIconProperty =
       BindableProperty.Create(
           nameof(ModuleFontFamilyIcon),
           typeof(string),
           typeof(ModuleCard),
           default(string));

    public string ModuleFontFamilyIcon
    {
        get => (string)GetValue(ModuleFontFamilyIconProperty);
        set => SetValue(ModuleFontFamilyIconProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
       BindableProperty.Create(
           nameof(Command),
           typeof(ICommand),
           typeof(ModuleCard),
           default(ICommand));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ModuleCard),
            default(object));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

}