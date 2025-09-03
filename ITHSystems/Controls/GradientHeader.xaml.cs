using System.Windows.Input;

namespace ITHSystems.Controls;

public partial class GradientHeader : ContentView
{
	public GradientHeader()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TitleProperty =
     BindableProperty.Create(
         nameof(Title),
         typeof(string),
         typeof(ModuleCard),
         default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    public static readonly BindableProperty BackButtonCommandProperty =
     BindableProperty.Create(
         nameof(BackButtonCommand),
         typeof(ICommand),
         typeof(ModuleCard),
         default(ICommand));

    public ICommand BackButtonCommand
    {
        get => (ICommand)GetValue(BackButtonCommandProperty);
        set => SetValue(BackButtonCommandProperty, value);
    }

    public static readonly BindableProperty BackButtonCommandParameterProperty =
     BindableProperty.Create(
         nameof(BackButtonCommandParameter),
         typeof(object),
         typeof(ModuleCard),false);

    public object BackButtonCommandParameter
    {
        get => GetValue(BackButtonCommandParameterProperty);
        set => SetValue(BackButtonCommandParameterProperty, value);
    }



}